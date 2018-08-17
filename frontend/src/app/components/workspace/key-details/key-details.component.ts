import { Component, OnInit, Input, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { ProjectService } from '../../../services/project.service';
import { IString } from '../../../models/string';
import { ComplexStringService } from '../../../services/complex-string.service';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Translation } from '../../../models';
import { ValueTransformer } from '../../../../../node_modules/@angular/compiler/src/util';
import { LanguageService } from '../../../services/language.service';

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

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(    private route: ActivatedRoute,
    private dataProvider: ComplexStringService,
    private languageService: LanguageService) 
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
        const temp = this.keyDetails.translations.length;
        for (var i = 0; i < temp; i++) {
          this.expandedArray.push(false);
        }
      });
     });
  }

  onSave(t: Translation){
    this.route.params.subscribe(value =>
      {
        debugger
        this.dataProvider.editStringTranslation(t, value.keyId)
          .subscribe(
          (d: Translation[])=> {
            console.log(d);
          },
          err => {
            console.log('err', err);
          }
        ); 
    console.log(this.keyDetails.translations);
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