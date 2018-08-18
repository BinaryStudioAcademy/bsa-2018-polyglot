import { Component, OnInit, Input, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { ProjectService } from '../../../services/project.service';
import { IString } from '../../../models/string';
import { ComplexStringService } from '../../../services/complex-string.service';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Translation, Project, Language } from '../../../models';
import { ValueTransformer } from '../../../../../node_modules/@angular/compiler/src/util';
import { LanguageService } from '../../../services/language.service';
import { elementAt } from 'rxjs/operators';

@Component({
  selector: 'app-workspace-key-details',
  templateUrl: './key-details.component.html',
  styleUrls: ['./key-details.component.sass']
})
export class KeyDetailsComponent implements OnInit, OnDestroy {

  public keyDetails: any; 
  public translationsDataSource: MatTableDataSource<any>; 
  public IsEdit : boolean = false;
  public IsPagenationNeeded: boolean = true;
  public pageSize: number  = 5;
  public Id : string;
  expandedArray = [];
  projectId: number;
  languages: Language[];
  translationLang: any;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(    private route: ActivatedRoute,
    private dataProvider: ComplexStringService,
    private projectService: ProjectService) 
  { 
    this.Id = this.route.snapshot.queryParamMap.get('keyid');
    this.expandedArray  
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

  setStep(index: number) {
    this.expandedArray[index] = true;
  }


  ngOnInit() {
     this.route.params.subscribe(value =>
     {
      this.dataProvider.getById(value.keyId).subscribe((data: any) => {
        this.keyDetails = data;
        this.projectId = this.keyDetails.projectId;
        const temp = this.keyDetails.translations.length;
        for (var i = 0; i < temp; i++) {
          this.expandedArray.push(false);
        }
        this.getLanguages();
      });
     });
  }

  getLanguages() {
    this.projectService.getProjectLanguages(this.projectId).subscribe(
      (d: Language[])=> {
        this.languages = d.map(x => Object.assign({}, x));
        this.setLanguagesInWorkspace();
      },
      err => {
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
    debugger
    console.log(this.keyDetails.translations);
  }
      
  getProp(id : number) {
    const searchedElement = this.keyDetails.translations.filter(el => el.languageId === id);
    return searchedElement.length > 0 ? searchedElement[0]: null;    
  }
  
  onSave(t: any){
    this.route.params.subscribe(value =>
    {
      debugger
        if(t.id) {
          this.dataProvider.editStringTranslation(t, value.keyId)
            .subscribe(
            (d: any[])=> {
              console.log(this.keyDetails.translations);
            },
            err => {
              console.log('err', err);
            }
          ); 
        }
        else {
          debugger
          this.dataProvider.createStringTranslation(t, value.keyId)
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
              },
              err => {
                console.log('err', err);
              }
            ); 
        }
    });
  }
  
  onClose(index: number) {
    this.expandedArray[index] = false;
  }
  ngOnDestroy() {
  }

  toggle(){
    this.IsEdit = !this.IsEdit;
  }

}