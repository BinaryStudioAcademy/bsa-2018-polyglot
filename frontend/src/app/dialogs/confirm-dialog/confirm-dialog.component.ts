import { Component, Input, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { KeyComponent } from '../../components/workspace/key/key.component';

export interface DialogData {
  description: string;
  btnOkText: string;
  btnCancelText: string;
  answer: boolean;
}

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.sass']
})

export class ConfirmDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<KeyComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  onCancelClick(): void {
    this.data.answer = false;
    this.dialogRef.close();
  }
  
  onOkClick(): void {
    this.data.answer = true;
    this.dialogRef.close();
  }
}