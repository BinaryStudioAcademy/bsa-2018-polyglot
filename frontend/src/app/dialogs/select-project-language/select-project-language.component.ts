import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '../../../../node_modules/@angular/material';
import {MatDialog, MatDialogConfig} from "@angular/material";

@Component({
  selector: 'app-select-project-language',
  templateUrl: './select-project-language.component.html',
  styleUrls: ['./select-project-language.component.sass']
})
export class SelectProjectLanguageComponent implements OnInit {

  selectedLangsIds: Array<number> = new Array<number>();
  @Output() onSelect = new EventEmitter<Array<number>>(true);
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
    this.onSelect.emit(this.selectedLangsIds);
  }

  change($event, id: number){
    debugger;
    let inArray = this.selectedLangsIds.includes(id);

    if($event.checked && !inArray)
      this.selectedLangsIds.push(id);
    else if(!$event.checked && inArray){
      this.selectedLangsIds = this.selectedLangsIds.filter(i => i != id);
    }
  }
  
  close(){
    this.dialogRef.close();
  }
}
