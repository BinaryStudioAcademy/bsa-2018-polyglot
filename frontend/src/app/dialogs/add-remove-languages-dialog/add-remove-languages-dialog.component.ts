import { Component, OnInit, Input, Inject } from '@angular/core';
import { TranslatorLanguage, Language } from '../../models';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { LanguageService } from '../../services/language.service';

@Component({
  selector: 'app-add-remove-languages-dialog',
  templateUrl: './add-remove-languages-dialog.component.html',
  styleUrls: ['./add-remove-languages-dialog.component.sass']
})
export class AddRemoveLanguagesDialogComponent implements OnInit {

    allLanguages: Language[];
    @Input() translatorLanguages: TranslatorLanguage[];
    assignedLanguages: TranslatorLanguage[] = [];
    notAssignedLanguages: TranslatorLanguage[] = [];
  
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<AddRemoveLanguagesDialogComponent>,
        private languageService: LanguageService
  ) { }

    ngOnInit() {
        if(this.data && this.data.translatorLanguages){
            this.translatorLanguages = this.data.translatorLanguages.map(x => Object.assign({}, x));
        }
        this.languageService.getAll().subscribe((languages)=>{
            this.allLanguages = languages;

            this.allLanguages.forEach(lang => {
                let index = this.translatorLanguages.findIndex(t => t.language.id === lang.id)
                if(index >= 0){
                    this.assignedLanguages.push(this.translatorLanguages[index])
                }
                else{
                    this.notAssignedLanguages.push({
                        proficiency: 0,
                        language: lang
                    });
                }
            });
        });
    }

}
