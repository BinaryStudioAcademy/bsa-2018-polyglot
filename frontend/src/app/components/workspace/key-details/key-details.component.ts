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
import { UserService } from '../../../services/user.service';
import { Observable } from '../../../../../node_modules/rxjs';
import * as signalR from '../../../../../node_modules/@aspnet/signalr';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-workspace-key-details',
  templateUrl: './key-details.component.html',
  styleUrls: ['./key-details.component.sass']
})

export class KeyDetailsComponent implements OnInit, OnDestroy {

  public keyDetails: any; 
  public translationsDataSource: MatTableDataSource<any>; 
  public IsPagenationNeeded: boolean = true;
  public pageSize: number  = 5;
  public IsEdit : boolean = false;
  public Id : string;
  public isEmpty
  public someOneTranslating: boolean = false;
  public editingUsers: any = [];
  private connection;
  private user;
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

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(TabHistoryComponent) history: TabHistoryComponent;


  constructor(
    private router : Router,
    private route: ActivatedRoute,
    private dataProvider: ComplexStringService,
    private projectService: ProjectService,
    public dialog: MatDialog,
    private snotifyService: SnotifyService,
    private userService: UserService
  ) { 
    debugger;
      this.user = userService.getCurrrentUser();
// ===================================================================
      //get the connection from router params

      this.connection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.apiUrl}/workspaceHub/`).build();

      // get the connection from router params
// ===================================================================
      
  }

  subscribeChanges(){
    if(this.connection)
        {

          this.connection.on("translating", (translatingById: number, translatingByFullName: string) => 
          {
            debugger;
         //   if(translatingById !== this.user.id)
         //   {
              this.editingUsers.push({
                id: translatingById,
                fullName: translatingByFullName
              });
              this.someOneTranslating = true;
         //   }
              
          });
          
          this.connection.on("stringTranslated", (message: string) => 
          {
            this.snotifyService.info(message , "Translated")
          });

        //  this.connection.on("newLanguage", (message: string) => 
        //  {
        //    this.snotifyService.info(message , "Language added")
        //  });
//
        //  this.connection.on("languageDeleted", (message: string) => 
        //  {
        //    this.snotifyService.info(message , "Language removed")
        //  });

          this.connection.on("newTranslation", (message: string) => 
          {
            this.snotifyService.info(message , "Translated")
          });

          // this.connection.on("newTranslation", (translatingById: number, translatingByFullName: string) => {
          //   if(translatingById !== this.user.id)
          //     this.editingUsers[translatingById] = translatingByFullName;
          // });
        }
  }

  ngOnChanges(){
    debugger;

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

  setStep(index: number) {
    this.expandedArray[index] = { isOpened: true, oldValue: this.keyDetails.translations[index].translationValue };
    this.history.showHistory(index);
  }


  ngOnInit() {
     this.route.params.subscribe(value =>
     {
       this.keyId = value.keyId;
       this.dataProvider.getById(value.keyId).subscribe((data: any) => {
        this.isLoad = false;
        this.keyDetails = data;
        this.projectId = this.keyDetails.projectId;
        this.getLanguages();
        
      });
     });
  }

  getLanguages() {
    this.projectService.getProjectLanguages(this.projectId).subscribe(
      (d: Language[])=> {
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
          ...this.getProp(element.id)
        });
      }
    );
    this.isLoad = true;
  }
      
  getProp(id : number) {
    const searchedElement = this.keyDetails.translations.filter(el => el.languageId === id);
    return searchedElement.length > 0 ? searchedElement[0]: null;    
  }
  
  onSave(index: number, t: Translation){
    debugger;
    
    // this.route.params.subscribe(value =>
    // {
        if(t.id!="00000000-0000-0000-0000-000000000000"&&t.id) {
          this.dataProvider.editStringTranslation(t, this.keyId)
            .subscribe(
            (d: any[])=> {
              console.log(this.keyDetails.translations);
              this.expandedArray[index] = { isOpened: false, oldValue: ''};
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
              (d: any)=> {
                const lenght = this.keyDetails.translations.length;
                for (var i = 0; i < lenght; i++) {
                  if(this.keyDetails.translations[i].languageId === d.languageId) {
                    this.keyDetails.translations[i] = {
                      languageName: this.keyDetails.translations[i].languageName,
                      ...d
                    };
                  }
                }
                console.log(this.keyDetails.translations);
                this.expandedArray[index] = { isOpened: false, oldValue: ''};
              },
              err => {
                console.log('err', err);
              }
            ); 
        }
    //});
  }
  
  onClose(index: number, translation: any) {
    debugger;
    
    if(this.expandedArray[index].oldValue == translation.translationValue){
      this.expandedArray[index].isOpened = false;
      return;
    }
     const dialogRef = this.dialog.open(SaveStringConfirmComponent, {
      width: '500px',
      data: {description: this.description, btnYesText: this.btnYesText, btnNoText: this.btnNoText,  btnCancelText: this.btnCancelText, answer: this.answer}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (dialogRef.componentInstance.data.answer === 1){
        this.onSave(index, translation);
      }
      else if(dialogRef.componentInstance.data.answer === 0) {
        this.keyDetails.translations[index].translationValue = this.expandedArray[index].oldValue;
        this.expandedArray[index] = { isOpened: false, oldValue: ''};
      }
    });
  }

  toggle(){
    this.IsEdit = !this.IsEdit;
  }

  change(t){
    // проверяем изменена ли строка
    // если да то кидаем всем уведомление
  }
  
  ngOnDestroy() {
  }

}

export interface TranslationState {
  isOpened: boolean;
  oldValue: string;
} 