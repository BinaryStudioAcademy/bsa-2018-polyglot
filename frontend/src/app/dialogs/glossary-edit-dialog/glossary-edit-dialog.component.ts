import { Component, OnInit, Inject } from '@angular/core';
import { Glossary } from '../../models';
import { GlossaryService } from '../../services/glossary.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-glossary-edit-dialog',
  templateUrl: './glossary-edit-dialog.component.html',
  styleUrls: ['./glossary-edit-dialog.component.sass']
})
export class GlossaryEditDialogComponent implements OnInit {


  public glossary: Glossary;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Glossary,
    private glossaryService: GlossaryService,
    public dialogRef: MatDialogRef<GlossaryEditDialogComponent>,
    private snotifyService: SnotifyService) { }


  ngOnInit() {
    this.glossary = this.data;
  }

  onSubmit(){

    this.glossaryService.update(this.glossary, this.glossary.id)
      .subscribe(
        (d) => {
          if(d)
          {
            this.snotifyService.success("Glossary edited", "Success!");
            this.dialogRef.close();     
          }
          else
          {
            this.snotifyService.success("Glossary wasn`t edited", "Error!");
            this.dialogRef.close();   
          }
              
        },
        err => {
          console.log('err', err);
          this.snotifyService.success("Glossary wasn`t edited", "Error!");
          this.dialogRef.close();     
        });
  }

  onDelete(){
    this.glossaryService.delete(this.glossary.id).subscribe(
      (d) => {
        if(d)
        {
          this.snotifyService.success("Glossary deleted", "Success!");
          this.dialogRef.close();     
        }
        else
        {
          this.snotifyService.success("Glossary wasn`t deleted", "Error!");
          this.dialogRef.close();   
        }
            
      },
      err => {
        console.log('err', err);
        this.snotifyService.success("Glossary wasn`t deleted", "Error!");
        this.dialogRef.close();     
      });
  }
}
