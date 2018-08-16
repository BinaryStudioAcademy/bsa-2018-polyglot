import { Component, OnInit, Inject, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '../../../../node_modules/@angular/material';

@Component({
  selector: 'app-select-project-language',
  templateUrl: './select-project-language.component.html',
  styleUrls: ['./select-project-language.component.sass']
})
export class SelectProjectLanguageComponent implements OnInit {

  langs = [
    {
      name: "Turkish"
    },
    {
      name: "Albanian"
    },
    {
      name: "Armenian"
    },
    {
      name: "Greek"
    },
    {
      name: "Polish"
    },
    {
      name: "Romanian"
    },
    {
      name: "Turkish"
    },
    {
      name: "Albanian"
    },
    {
      name: "Armenian"
    },
    {
      name: "Greek"
    },
    {
      name: "Polish"
    },
    {
      name: "Romanian"
    }
  ]
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<SelectProjectLanguageComponent>
  ) { 
    if(data && data.langs)
      this.langs = data.langs;
  }

  ngOnInit() {
  }

  select(){

  }

  
  close(){
    this.dialogRef.close();
  }
}
