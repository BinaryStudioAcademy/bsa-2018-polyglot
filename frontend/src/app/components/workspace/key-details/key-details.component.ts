import { Component, OnInit, Input, OnDestroy, ViewChild, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource, MatPaginator, MatDialog } from '@angular/material';
import { ProjectService } from '../../../services/project.service';
import { ComplexStringService } from '../../../services/complex-string.service';
import { Translation, Language } from '../../../models';
import { SnotifyService } from 'ng-snotify';
import { SaveStringConfirmComponent } from '../../../dialogs/save-string-confirm/save-string-confirm.component';
import { TabHistoryComponent } from './tab-history/tab-history.component';
import { TranslationType } from '../../../models/TranslationType';
import { AppStateService } from '../../../services/app-state.service';
import { environment } from '../../../../environments/environment';
import * as signalR from '../../../../../node_modules/@aspnet/signalr';
import { SignalrService } from '../../../services/signalr.service';
import { TranslationState } from '../../../models/translation-state';
import { TranslationService } from '../../../services/translation.service';
import { SignalrSubscribeActions } from '../../../models/signalrModels/signalr-subscribe-actions';
import { TabGlossaryComponent } from './tab-glossary/tab-glossary.component';

@Component({
    selector: 'app-workspace-key-details',
    templateUrl: './key-details.component.html',
    styleUrls: ['./key-details.component.sass']
})

export class KeyDetailsComponent implements OnInit {

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(TabHistoryComponent) history: TabHistoryComponent;

    public keyDetails: any;
    public translationsDataSource: MatTableDataSource<any>;
    public IsEdit: boolean = false;
    public IsPagenationNeeded: boolean = true;
    public pageSize: number = 5;
    public Id: string;
    public isEmpty: boolean;
    projectId: number;
    languages: Language[];
    expandedArray: TranslationState[];
    isLoad: boolean;
    comments: Comment[];
    description: string = "Do you want to save changes?";
    btnYesText: string = "Yes";
    btnNoText: string = "No";
    btnCancelText: string = "Cancel";
    answer: number;
    keyId: number;
    isDisabled: boolean;
    dataIsLoaded: boolean = false;
    isMachineTranslation: boolean;
    public MachineTranslation: string;
    public previousTranslation: string;
    currentTranslation: string;



    constructor(private route: ActivatedRoute,
        private dataProvider: ComplexStringService,
        public dialog: MatDialog,
        private snotifyService: SnotifyService,
        private appState: AppStateService,
        private router: Router,
        private signalrService: SignalrService,
        private projectService: ProjectService,
        private service: TranslationService
    ) {
    }

    ngOnInit() {
        if (this.appState.getWorkspaceState === null) {
            this.dataIsLoaded = false;
            this.router.navigate([`/workspace/${this.projectId}`]);
            return;
        }
        this.dataIsLoaded = true;
        this.isMachineTranslation = false;

        this.route.params.subscribe(value => {
            this.keyId = value.keyId;
            this.dataProvider.getById(value.keyId).subscribe((data: any) => {
                this.isLoad = false;
                this.keyDetails = data;
                this.projectId = this.keyDetails.projectId;
                this.signalrService.createConnection(this.keyDetails.id, 'workspaceHub');
                this.subscribeProjectChanges();
                this.getLanguages();
            });
            this.dataProvider.getCommentsByStringId(this.keyId).subscribe(comments => {
                this.comments = comments;
            });
        });
    }

    ngOnDestroy() {
        if (this.dataIsLoaded) {
            this.signalrService.closeConnection(this.keyDetails.id);
        }
    }


    ngOnChanges(){
        if(this.keyDetails && this.keyDetails.translations){
          this.IsPagenationNeeded = this.keyDetails.translations.length > this.pageSize;
          this.translationsDataSource = new MatTableDataSource(this.keyDetails.translations);

          if(this.IsPagenationNeeded){
            this.paginator.pageSize = this.pageSize;
            this.translationsDataSource.paginator = this.paginator;
          }

        }
        else
          this.IsPagenationNeeded = false;
      }



    subscribeProjectChanges() {
        this.signalrService.connection.on(SignalrSubscribeActions[SignalrSubscribeActions.changedTranslation], (translation: any) => {
            this.setNewValueTranslation(translation);
        });
        this.signalrService.connection.on(SignalrSubscribeActions[SignalrSubscribeActions.commentAdded], (comments: any) => {
            this.comments = comments;
        });
    }



  setStep(index: number) {
    this.expandedArray[index] = { isOpened: true, oldValue: this.keyDetails.translations[index].translationValue };
    for (let i = 0; i < this.expandedArray.length; i++) {
        if (i != index) {
            this.expandedArray[i].isOpened = false;
        }
    }
    this.history.showHistory(index);
    this.currentTranslation = this.keyDetails.translations[index].translationValue;
  }





    setNewValueTranslation(translation: any) {
        const lenght = this.keyDetails.translations.length;
        for (var i = 0; i < lenght; i++) {
            if (this.keyDetails.translations[i].languageId === translation.languageId) {
                this.keyDetails.translations[i] = {
                    languageName: this.keyDetails.translations[i].languageName,
                    languageId: this.keyDetails.translations[i].languageId,
                    ...translation
                };
            }
        }
    }

    getLanguages() {
        this.languages = this.appState.getWorkspaceState.languages;

        const temp = this.languages.length;
        this.expandedArray = new Array();
        for (var i = 0; i < temp; i++) {
            this.expandedArray.push({ isOpened: false, oldValue: '' });
        }
        this.isEmpty = false;
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
            this.currentTranslation = '';
  
            if (this.isMachineTranslation) {
                t.Type = TranslationType.Machine;
                this.isMachineTranslation = false;
            }
            else {
                t.Type = TranslationType.Human;

        if (t.id != "00000000-0000-0000-0000-000000000000" && t.id) {
            this.dataProvider.editStringTranslation(t, this.keyId)
                .subscribe(
                    (d: any[]) => {
                        //console.log(this.keyDetails.translations);
                        this.expandedArray[index] = { isOpened: false, oldValue: '' };
                    },
                    err => {
                        this.snotifyService.error(err);
                    }
                );
        }
        else {
            t.createdOn = new Date();
            this.dataProvider.createStringTranslation(t, this.keyId)
                .subscribe(
                    (d: any) => {
                        this.expandedArray[index] = { isOpened: false, oldValue: '' };
                    },
                    err => {
                        console.log('err', err);
                    }
                );
        }
    }
    }
    onClose(index: number, translation: any) {
        
        if (this.expandedArray[index].oldValue == translation.translationValue && !this.isMachineTranslation) {
            this.expandedArray[index].isOpened = false;
            this.currentTranslation = '';
            return;
        }
        const dialogRef = this.dialog.open(SaveStringConfirmComponent, {
            width: '500px',
            data: { description: this.description, btnYesText: this.btnYesText, btnNoText: this.btnNoText, btnCancelText: this.btnCancelText, answer: this.answer }
        });
        dialogRef.afterClosed().subscribe(result => {
            if (dialogRef.componentInstance.data.answer === 1) {
                this.onSave(index, translation);
                this.isMachineTranslation = false;
            }
            else if (dialogRef.componentInstance.data.answer === 0) {
                this.keyDetails.translations[index].translationValue = this.expandedArray[index].oldValue;
                this.expandedArray[index] = { isOpened: false, oldValue: '' };
                if(this.isMachineTranslation){
                    this.keyDetails.translations[index].translationValue = this.previousTranslation;
                    this.isMachineTranslation = false;
                }
                this.currentTranslation = '';
            }
        });
        
    }



    onMachineTranslationMenuClick(item: any): void {
        this.service.getTransation({ q: this.keyDetails.base, target: item }).subscribe((res: any) => {
            this.MachineTranslation = res[0].translatedText;
        })
    }

    toggle() {
        this.IsEdit = !this.IsEdit;
    }
    selectTranslation($event) {

        this.previousTranslation = this.keyDetails.translations[$event.keyId].translationValue;

        this.isMachineTranslation = true;

        this.keyDetails.translations[$event.keyId].translationValue = $event.translation;

        this.expandedArray[$event.keyId].isOpened = true;
    }


  toggleDisable() {
    this.isDisabled = !this.isDisabled;
  }

  highlightString(index: number) {
    if (this.expandedArray[index].isOpened) {
      return '2px ridge #6495ED';
    }
    return '';
  }
}



// this.signalrService.connection.on(
        //   "addedFirstTranslation",
        //   (translation: any) => {
        //     debugger
        //     const lang = this.languages.filter(el => el.id === translation.languageId)[0];
        //     this.keyDetails.translations.filter(el => el.languageId === translation.languageId)[0]
        //       = {
        //         languageName: lang.name,
        //         languageId: lang.id,
        //         ...translation
        //       };
        //     // получить строку с сервера, вывести уведомление
        //     this.snotifyService.info("String translated", "Translated");
        //   }
        // );
        // this.connection.on("stringDeleted", (deletedStringId: number) => {

        //     // ================> проверить id если та строка
        //     // ================> на которой мы сейачас находимся то перенаправить на воркспейс
        //     if (deletedStringId === this.keyId) {
        //         this.snotifyService.info(
        //             `This string(id:${deletedStringId}) is deleted!`,
        //             "String deleted"
        //         );
        //         // ===============>
        //     }
        // });

        // this.connection.on(
        //     "stringTranslated",
        //     (complexStringId: number, languageId: number) => {
        //         // получить строку с сервера, вывести уведомление
        //         this.snotifyService.info("String translated", "Translated");
        //     }
        // );

        // this.connection.on("languageAdded", (languagesIds: Array<number>) => {
        //     // обновить строку
        //     console.log(languagesIds);
        //     this.snotifyService.info(languagesIds.join(", "), "Language added");
        // });

        // this.connection.on("languageDeleted", (languageId: number) => {
        //     // обновить строку
        //     this.snotifyService.info(
        //         `lang with id =${languageId} removed`,
        //         "Language removed"
        //     );
        // });
