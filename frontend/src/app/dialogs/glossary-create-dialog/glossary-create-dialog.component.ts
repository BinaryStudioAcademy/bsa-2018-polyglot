import { Component, OnInit, Output, Inject } from '@angular/core';
import { Glossary, Language } from '../../models';
import { EventEmitter } from 'protractor';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { GlossaryService } from '../../services/glossary.service';
import { SnotifyService } from 'ng-snotify';
import { LanguageService } from '../../services/language.service';
import { AppStateService } from '../../services/app-state.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-glossary-create-dialog',
    templateUrl: './glossary-create-dialog.component.html',
    styleUrls: ['./glossary-create-dialog.component.sass']
})
export class GlossaryCreateDialogComponent implements OnInit {

  public glossary: Glossary;
  languages: Language[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private glossaryService: GlossaryService,
    public dialogRef: MatDialogRef<GlossaryCreateDialogComponent>,
    private snotifyService: SnotifyService,
    private languageService: LanguageService,
    private stateService: AppStateService,
    private router: Router) { }


  ngOnInit() {
    this.languageService.getAll()
    .subscribe(
    (d: Language[])=> {
      this.languages = d.map(x => Object.assign({}, x));
    },
    err => {
      console.log('err', err);
    }
  );  
    this.glossary = {
      id: 0,
      name: '',
      originLanguage: {id : 0, code : '', name : ''},
      projectGlossaries: [],
      glossaryStrings: [],
      userProfile: this.stateService.currentDatabaseUser
      
    };
  }

  onSubmit(){
        this.glossaryService.create(this.glossary)
            .subscribe(
                (d) => {
                    this.snotifyService.success("Glossary created", "Success!");
                    this.dialogRef.close();
                    this.router.navigate(['dashboard/glossaries', d.id]);
                },
                err => {
                    console.log('err', err);
                    this.snotifyService.error("Glossary wasn`t created", "Error!");
                    this.dialogRef.close();
                });
  }

