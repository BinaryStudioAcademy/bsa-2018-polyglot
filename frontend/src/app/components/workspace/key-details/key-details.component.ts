import { Component, OnInit, Input, OnDestroy, ViewChild, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource, MatPaginator, MatDialog } from '@angular/material';
import { ProjectService } from '../../../services/project.service';
import { IString } from '../../../models/string';
import { ComplexStringService } from '../../../services/complex-string.service';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Translation, Project, Language } from '../../../models';
import { ValueTransformer } from '../../../../../node_modules/@angular/compiler/src/util';
import { LanguageService } from '../../../services/language.service';
import { elementAt } from 'rxjs/operators';
import { SnotifyService } from 'ng-snotify';
import { SaveStringConfirmComponent } from '../../../dialogs/save-string-confirm/save-string-confirm.component';
import { TabHistoryComponent } from './tab-history/tab-history.component';
import { AppStateService } from '../../../services/app-state.service';
import { HubConnection } from '../../../../../node_modules/@aspnet/signalr';
import { environment } from '../../../../environments/environment';
import * as signalR from '../../../../../node_modules/@aspnet/signalr';

@Component({
    selector: 'app-workspace-key-details',
    templateUrl: './key-details.component.html',
    styleUrls: ['./key-details.component.sass']
})

export class KeyDetailsComponent implements OnInit, OnDestroy {

    public keyDetails: any;
    public translationsDataSource: MatTableDataSource<any>;
    public IsEdit: boolean = false;
    public IsPagenationNeeded: boolean = true;
    public pageSize: number = 5;
    public Id: string;
    public isEmpty
    connection: any;
    projectId: number;
    languages: Language[];
    expandedArray: Array<TranslationState>;
    isLoad: boolean;

    description: string = "Do you want to save changes?";
    btnYesText: string = "Yes";
    btnNoText: string = "No";
    btnCancelText: string = "Cancel";
    answer: number;
    keyId: number;
    isDisabled: boolean;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(TabHistoryComponent) history: TabHistoryComponent;

    constructor(private route: ActivatedRoute,
        private dataProvider: ComplexStringService,
        private projectService: ProjectService,
        public dialog: MatDialog,
        private snotifyService: SnotifyService,
        private appState: AppStateService,
        private router: Router,
    ) {
        this.Id = this.route.snapshot.queryParamMap.get('keyid');
    }


    ngOnChanges() {
        if (this.keyDetails && this.keyDetails.translations) {
            this.IsPagenationNeeded = this.keyDetails.translations.length > this.pageSize;
            this.translationsDataSource = new MatTableDataSource(this.keyDetails.translations);

            if (this.IsPagenationNeeded) {
                this.paginator.pageSize = this.pageSize;
                this.translationsDataSource.paginator = this.paginator;
            }

        }
        else
            this.IsPagenationNeeded = false;
    }

    step = 0;

    setStep(index: number) {
        this.expandedArray[index] = { isOpened: true, oldValue: this.keyDetails.translations[index].translationValue };
        this.history.showHistory(index);
    }


    ngOnInit() {

        debugger;
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${environment.apiUrl}/workspaceHub/`)
            .build();

        this.connection.start().catch(err => console.log("ERROR " + err));
        this.connection.onclose(function(e){
          console.log("SignalR connection closed.Reconnecting....");
          this.connectSignalR();
        });

        this.route.params.subscribe(value => {
            this.keyId = value.keyId;
            this.dataProvider.getById(value.keyId).subscribe((data: any) => {
                this.isLoad = false;
                this.keyDetails = data;
                this.projectId = this.keyDetails.projectId;
                this.getLanguages();
            });
        });
    }

    ngOnDestroy() {1
        this.connection.send("leaveProjectGroup", `${this.projectId}`);
        this.connection.stop();
    }

    subscribeProjectChanges() {
        
        this.connection.send("joinProjectGroup", `${this.projectId}`);

        this.connection.on("stringDeleted", (deletedStringId: number) => {

// ================> проверить id если та строка 
// ================> на которой мы сейачас находимся то перенаправить на воркспейс
                if(deletedStringId === this.keyId){
                    this.snotifyService.info(
                        `This string(id:${deletedStringId}) is deleted!`,
                        "String deleted"
                    );
// ===============> 
                }
        });

        this.connection.on(
            "stringTranslated",
            (complexStringId: number, languageId: number) => {
                // получить строку с сервера, вывести уведомление
                this.snotifyService.info("String translated", "Translated");
            }
        );

        this.connection.on("languageAdded", (languagesIds: Array<number>) => {
            // обновить строку
            console.log(languagesIds);
            this.snotifyService.info(languagesIds.join(", "), "Language added");
        });

        this.connection.on("languageDeleted", (languageId: number) => {
            // обновить строку
            this.snotifyService.info(
                `lang with id =${languageId} removed`,
                "Language removed"
            );
        });
    }

    getLanguages() {
        debugger;
        if(this.appState.getWorkspaceState === null) {
            debugger
            this.router.navigate([`/workspace/${this.projectId}`]);
            return;
        }

        this.languages = this.appState.getWorkspaceState.languages;

        const temp = this.languages.length;
        this.expandedArray = new Array();
        for (var i = 0; i < temp; i++) {
            this.expandedArray.push({ isOpened: false, oldValue: '' });
        }
        this.isEmpty = false;
        this.setLanguagesInWorkspace();
    }

    setLanguagesInWorkspace() {
        this.keyDetails.translations = this.languages.map(
            element => {
                return ({
                    languageName: element.name,
                    languageId: element.id,
                    ...this.getProp(element.id)
                });
            }
        );
        this.isLoad = true;
    }

    getProp(id: number) {
        const searchedElement = this.keyDetails.translations.filter(el => el.languageId === id);
        return searchedElement.length > 0 ? searchedElement[0] : null;
    }

    onSave(index: number, t: any) {

        if (t.id != "00000000-0000-0000-0000-000000000000" && t.id) {
            this.dataProvider.editStringTranslation(t, this.keyId)
                .subscribe(
                    (d: any[]) => {
                        console.log(this.keyDetails.translations);
                        this.expandedArray[index] = { isOpened: false, oldValue: '' };
                    },
                    err => {
                        console.log('err', err);
                    }
                );
        }
        else {
            t.createdOn = new Date();
            this.dataProvider.createStringTranslation(t, this.keyId)
                .subscribe(
                    (d: any) => {
                        const lenght = this.keyDetails.translations.length;
                        for (var i = 0; i < lenght; i++) {
                            if (this.keyDetails.translations[i].languageId === d.languageId) {
                                this.keyDetails.translations[i] = {
                                    languageName: this.keyDetails.translations[i].languageName,
                                    ...d
                                };
                            }
                        }
                        console.log(this.keyDetails.translations);
                        this.expandedArray[index] = { isOpened: false, oldValue: '' };
                        debugger;
                       if(this.connection.connection.connectionState === 1)
                       {
                         this.connection.send(
                           "stringTranslated",
                           this.projectId,
                           this.keyId,
                           t.languageId

                       );

                       }
                       else{
                         this.connectSignalR();
                         this.connection.send(
                           "stringTranslated",
                           this.projectId,
                           this.keyId,
                           t.languageId
                       )
                   }
                   
                   console.log("String translated..");
                    },
                    err => {
                        console.log('err', err);
                    }
                );

        }
        //});
    }

    onClose(index: number, translation: any) {
        if (this.expandedArray[index].oldValue == translation.translationValue) {
            this.expandedArray[index].isOpened = false;
            return;
        }
        const dialogRef = this.dialog.open(SaveStringConfirmComponent, {
            width: '500px',
            data: { description: this.description, btnYesText: this.btnYesText, btnNoText: this.btnNoText, btnCancelText: this.btnCancelText, answer: this.answer }
        });
        dialogRef.afterClosed().subscribe(result => {
            if (dialogRef.componentInstance.data.answer === 1) {
                this.onSave(index, translation);
            }
            else if (dialogRef.componentInstance.data.answer === 0) {
                this.keyDetails.translations[index].translationValue = this.expandedArray[index].oldValue;
                this.expandedArray[index] = { isOpened: false, oldValue: '' };
            }
        });
    }

    toggle() {
        this.IsEdit = !this.IsEdit;
    }

    toggleDisable() {
        this.isDisabled = !this.isDisabled;
    }

    connectSignalR(){
        this.connection.start().catch(err => console.log("ERROR " + err));
      }
}

export interface TranslationState {
    isOpened: boolean;
    oldValue: string;
}
