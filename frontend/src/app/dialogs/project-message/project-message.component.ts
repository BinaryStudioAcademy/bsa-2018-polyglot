import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '../../../../node_modules/@angular/material';

@Component({
  selector: 'app-project-message',
  templateUrl: './project-message.component.html',
  styleUrls: ['./project-message.component.sass']
})
export class ProjectMessageComponent  {

  constructor(
    public dialogRef: MatDialogRef<ProjectMessageComponent>) {}

  Shutdown(): void {
    this.dialogRef.close();
  }

}
