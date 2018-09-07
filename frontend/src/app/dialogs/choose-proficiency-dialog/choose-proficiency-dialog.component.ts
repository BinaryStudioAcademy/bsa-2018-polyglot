import { Component, OnInit, Output, Inject, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Language } from '../../models';
import { LanguageService } from '../../services/language.service';

@Component({
  selector: 'app-choose-proficiency-dialog',
  templateUrl: './choose-proficiency-dialog.component.html',
  styleUrls: ['./choose-proficiency-dialog.component.sass']
})
export class ChooseProficiencyDialogComponent implements OnInit {

  allLanguages: Language[];
  @Output() onSubmit = new EventEmitter<any>(true);
  langs = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<ChooseProficiencyDialogComponent>,
    private languageService: LanguageService
  ) { 

  }

  ngOnInit() {
    this.languageService.getAll().subscribe((languages)=>{
      this.allLanguages = languages;
    })
  }

  submit(){
    
    
  }

  change($event, lang){

    let inArray = this.selectedLangs.find(l => l.id === lang.id)

    if($event.checked && !inArray)
      this.selectedLangs.push(lang);
    else if(!$event.checked && inArray){
      this.selectedLangs = this.selectedLangs.filter(l => l.id != lang.id);
    }
  }
}
