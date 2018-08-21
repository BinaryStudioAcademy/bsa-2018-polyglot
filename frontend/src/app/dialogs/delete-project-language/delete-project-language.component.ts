import { Component, Input, Output, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { messaging } from 'firebase';

@Component({
  selector: 'app-delete-project-language',
  templateUrl: './delete-project-language.component.html',
  styleUrls: ['./delete-project-language.component.sass']
})
export class DeleteProjectLanguageComponent {

  translationsCount: number = 0;
  languageName: string;
  confirmed: boolean = false;
  message: string = "Deletion canceled";
  public inputLangName: string;
  @Output() onConfirmDelete = new EventEmitter<any>(true);

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<DeleteProjectLanguageComponent>
  ) {
    this.languageName = data.languageName;
    this.translationsCount = data.translationsCount;
   }


  confirm(){
    if(this.inputLangName && 
      this.inputLangName.toLocaleLowerCase().trim() == this.languageName.toLocaleLowerCase().trim())
    {
      this.message = "The data is being deleted";
      this.confirmed = true;
    }
    else
    {
      this.message  = "Deletion canceled.The entered string does not match the name of the language to remove";
      this.confirmed = false;
    }

    this.onConfirmDelete.emit({
      state: this.confirmed,
      message: this.message
    });
    this.dialogRef.close();
  }
}
