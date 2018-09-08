import { Component, OnInit, Output, Inject, EventEmitter, Input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Language, TranslatorLanguage } from '../../models';
import { LanguageService } from '../../services/language.service';
import { Proficiency } from '../../models/proficiency';

@Component({
    selector: 'app-choose-proficiency-dialog',
    templateUrl: './choose-proficiency-dialog.component.html',
    styleUrls: ['./choose-proficiency-dialog.component.sass']
})
export class ChooseProficiencyDialogComponent implements OnInit {

    allLanguages: Language[];
    @Output() onSubmit = new EventEmitter<any>(true);
    @Input() translatorLanguages: TranslatorLanguage[];
    newTranslatorLanguages: TranslatorLanguage[] = [];

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<ChooseProficiencyDialogComponent>,
        private languageService: LanguageService
    ) { }

    ngOnInit() {
        if(this.data && this.data.translatorLanguages){
            this.translatorLanguages = this.data.translatorLanguages;
        }
        this.languageService.getAll().subscribe((languages)=>{
            this.allLanguages = languages;

            this.allLanguages.forEach(lang => {
                this.newTranslatorLanguages.push({
                    proficiency: null,
                    language: lang
                })
            });
            console.log(this.newTranslatorLanguages);
            console.log(this.translatorLanguages);
            console.log(this.allLanguages)
        })  
    }

    getProficiencyValues(): string[]{

        const profs = Object.keys(Proficiency).filter((item) => {
            return isNaN(Number(item));
        });
        return profs;
    }

    proficiencyChange(prof: string, translatorLanguage: TranslatorLanguage){
        translatorLanguage.proficiency = Proficiency[prof];
        console.log(this.newTranslatorLanguages);
    }

    submit(){
  
    }
}
