import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { GlossaryService } from '../../services/glossary.service';
import { GlossaryCreateDialogComponent } from '../glossary-create-dialog/glossary-create-dialog.component';
import { SnotifyService } from 'ng-snotify';
import { Glossary } from '../../models';
import { GlossaryString } from '../../models/glossary-string';

@Component({
  selector: 'app-glossary-string-dialog',
  templateUrl: './glossary-string-dialog.component.html',
  styleUrls: ['./glossary-string-dialog.component.sass']
})
export class GlossaryStringDialogComponent implements OnInit {

  public string: GlossaryString;
  public glossary: number;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private glossaryService: GlossaryService,
    public dialogRef: MatDialogRef<GlossaryStringDialogComponent>,
    private snotifyService: SnotifyService) { }

  ngOnInit() {
    this.string = this.data.string;
    this.glossary = this.data.glossaryId;
  }

  onSubmit(){
    if(this.string.id){
      console.log(this.string);
      this.glossaryService.editString(this.glossary, this.string)
      .subscribe(
        (d) => {
          if(d)
          {
            this.snotifyService.success("Glossary string edited", "Success!");
            this.dialogRef.close();     
          }
          else
          {
            this.snotifyService.error("Glossary string wasn`t edited", "Error!");
            this.dialogRef.close();   
          }
              
        },
        err => {
          console.log('err', err);
          this.snotifyService.error("Glossary string wasn`t edited", "Error!");
          this.dialogRef.close();     
        });
    } else{
      this.glossaryService.addString(this.glossary, this.string)
      .subscribe(
        (d) => {
          if(d)
          {
            this.snotifyService.success("Glossary string created", "Success!");
            this.dialogRef.close();     
          }
          else
          {
            this.snotifyService.error("Glossary string wasn`t created", "Error!");
            this.dialogRef.close();   
          }
              
        },
        err => {
          console.log('err', err);
          this.snotifyService.error("Glossary string wasn`t created", "Error!");
          this.dialogRef.close();     
        });
    }

  }

}
