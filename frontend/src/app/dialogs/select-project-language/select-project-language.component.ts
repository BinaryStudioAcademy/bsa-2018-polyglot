import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '../../../../node_modules/@angular/material';
import {MatDialog, MatDialogConfig} from "@angular/material";

@Component({
  selector: 'app-select-project-language',
  templateUrl: './select-project-language.component.html',
  styleUrls: ['./select-project-language.component.sass']
})
export class SelectProjectLanguageComponent implements OnInit {

  selectedLangs = [];
  @Output() onSelect = new EventEmitter<any>(true);
  langs = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<SelectProjectLanguageComponent>
  ) { 
    if(data && data.langs)
      this.langs = data.langs;
  }

  ngOnInit() {
  }

  submit(){
    debugger;
    this.onSelect.emit(this.selectedLangs);
    this.dialogRef.close();
  }

  change($event, lang){

    let inArray = this.selectedLangs.find(l => l.id === lang.id)

    if($event.checked && !inArray)
      this.selectedLangs.push(lang);
    else if(!$event.checked && inArray){
      this.selectedLangs = this.selectedLangs.filter(l => l.id != lang.id);
    }
  }
  
  close(){
    this.dialogRef.close();
  }
}
