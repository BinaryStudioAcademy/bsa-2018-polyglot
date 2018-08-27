import { Component, OnInit, Output, Inject } from '@angular/core';
import { Glossary } from '../../models';
import { EventEmitter } from 'protractor';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { GlossaryService } from '../../services/glossary.service';
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-glossary-create-dialog',
  templateUrl: './glossary-create-dialog.component.html',
  styleUrls: ['./glossary-create-dialog.component.sass']
})
export class GlossaryCreateDialogComponent implements OnInit {

  public glossary: Glossary;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private glossaryService: GlossaryService,
    public dialogRef: MatDialogRef<GlossaryCreateDialogComponent>,
    private snotifyService: SnotifyService) { }


  ngOnInit() {
    this.glossary = {
      id: 0,
      name: '',
      originLanguage: '',
      projectGlossaries: [],
      glossaryStrings: []
      
    };
  }

  onSubmit(){

    this.glossaryService.create(this.glossary)
      .subscribe(
        (d) => {
          if(d)
          {
            this.snotifyService.success("Glossary created", "Success!");
            this.dialogRef.close();     
          }
          else
          {
            this.snotifyService.error("Glossary wasn`t created", "Error!");
            this.dialogRef.close();   
          }
              
        },
        err => {
          console.log('err', err);
          this.snotifyService.error("Glossary wasn`t created", "Error!");
          this.dialogRef.close();     
        });
  }
}
