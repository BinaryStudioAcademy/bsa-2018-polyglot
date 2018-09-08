import { Component, OnInit, Output, Inject, EventEmitter, Input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Language, Proficiency, TranslatorLanguage } from '../../models';
import { LanguageService } from '../../services/language.service';

@Component({
  selector: 'app-choose-proficiency-dialog',
  templateUrl: './choose-proficiency-dialog.component.html',
  styleUrls: ['./choose-proficiency-dialog.component.sass']
})
export class ChooseProficiencyDialogComponent implements OnInit {

  allLanguages: Language[];
  @Output() onSubmit = new EventEmitter<any>(true);
  @Input() translatorLanguages: TranslatorLanguage[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<ChooseProficiencyDialogComponent>,
    private languageService: LanguageService
  ) { 

  }

  ngOnInit() {
    if(this.data && this.data.translatorLanguages){
      this.translatorLanguages = this.data.translatorLanguages;
    }
    this.languageService.getAll().subscribe((languages)=>{
      this.allLanguages = languages;
      console.log(this.translatorLanguages);
      console.log(this.allLanguages)
    })
    
  }

  submit(){
    
    
  }
}
