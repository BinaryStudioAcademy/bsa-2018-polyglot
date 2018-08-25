
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource, MatPaginator, MatDialog, } from '@angular/material';
import { ProjectService } from '../../../services/project.service';
import { ComplexStringService } from '../../../services/complex-string.service';
import { Translation, Language } from '../../../models';
import { SnotifyService } from 'ng-snotify';
import { SaveStringConfirmComponent } from '../../../dialogs/save-string-confirm/save-string-confirm.component';
import { TabHistoryComponent } from './tab-history/tab-history.component';
import { TranslationType } from '../../../models/TranslationType';
import { AppStateService } from '../../../services/app-state.service';
import { TranslationService } from '../../../services/translation.service';
import { TabGlossaryComponent } from './tab-glossary/tab-glossary.component';

@Component({
    selector: 'app-workspace-key-details',
    templateUrl: './key-details.component.html',
    styleUrls: ['./key-details.component.sass']
})

export class KeyDetailsComponent implements OnInit {

    public keyDetails: any;
    public translationsDataSource: MatTableDataSource<any>;
    public IsEdit: boolean = false;
    public IsPagenationNeeded: boolean = true;
    public pageSize: number = 5;
    public Id: string;
    public isEmpty;
    public MachineTranslation: string;
    public previousTranslation: string;
    projectId: number;
    languages: Language[];
    expandedArray: Array<TranslationState>;
    isLoad: boolean;
    isMachineTranslation: boolean;

    description: string = "Do you want to save changes?";
    btnYesText: string = "Yes";
    btnNoText: string = "No";
    btnCancelText: string = "Cancel";
    answer: number;
    keyId: number;
    isDisabled: boolean = false;

    currentTranslation: string;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(TabHistoryComponent) history: TabHistoryComponent;
    @ViewChild(TabGlossaryComponent) glossary: TabGlossaryComponent;

    constructor(private route: ActivatedRoute,
        private dataProvider: ComplexStringService,
        private projectService: ProjectService,
        public dialog: MatDialog,
        private snotifyService: SnotifyService,
        private appState: AppStateService,
        private service: TranslationService) {
        this.Id = this.route.snapshot.queryParamMap.get('keyid');
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
    
      step = 0;

      ngOnInit() {
        this.isMachineTranslation = false;
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


    getLanguages() {

        this.projectService.getProjectLanguages(this.projectId).subscribe(
            (d: Language[]) => {
                const temp = d.length;
                this.expandedArray = new Array();
                for (var i = 0; i < temp; i++) {
                    this.expandedArray.push({ isOpened: false, oldValue: '' });
                }
                this.languages = d.map(x => Object.assign({}, x));
                this.isEmpty = false;
                console.log(this.isEmpty);
                this.setLanguagesInWorkspace();
            },
            err => {
                this.isEmpty = true;
                console.log('err', err);
            }
        );

    }

    setLanguagesInWorkspace() {
        this.keyDetails.translations = this.languages.map(
            element => {
                return ({
                    languageName: element.name,
                    languageId: element.id,
                    languageCode: element.code,
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

    onSave(index: number, t: Translation) {
        this.currentTranslation = '';

        if (this.isMachineTranslation) {
            t.Type = TranslationType.Machine;
            this.isMachineTranslation = false;
        }
        else {
            t.Type = TranslationType.Human;
        }

        if (t.id != "00000000-0000-0000-0000-000000000000" && t.id) {
            t.userId = this.appState.currentDatabaseUser.id;
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
            t.userId = this.appState.currentDatabaseUser.id;
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
                    },
                    err => {
                        console.log('err', err);
                    }
                );
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

    toggle() {
        this.IsEdit = !this.IsEdit;
    }

    onMachineTranslationMenuClick(item: any): void {
        this.service.getTransation({ q: this.keyDetails.base, target: item }).subscribe((res: any) => {
            this.MachineTranslation = res[0].translatedText;
        })
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


export interface TranslationState {
    isOpened: boolean;
    oldValue: string;
} 