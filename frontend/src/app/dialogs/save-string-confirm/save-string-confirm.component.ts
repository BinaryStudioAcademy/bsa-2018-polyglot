import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { KeyDetailsComponent } from '../../components/workspace/key-details/key-details.component';

export interface DialogData {
  description: string;
  btnYesText: string;
  btnNoText: string;
  btnCancelText: string;
  answer: number;
}


@Component({
  selector: 'app-save-string-confirm',
  templateUrl: './save-string-confirm.component.html',
  styleUrls: ['./save-string-confirm.component.sass']
})



export class SaveStringConfirmComponent implements OnInit {
  ngOnInit(): void {
  }

  constructor(
    public dialogRef: MatDialogRef<KeyDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  onCancelClick(): void {
    this.data.answer = 2;
    this.dialogRef.close();
  }
  
  onYesClick(): void {
    this.data.answer = 1;
    this.dialogRef.close();
  }

  onNoClick(): void {
    this.data.answer = 0;
    this.dialogRef.close();
  }
}
