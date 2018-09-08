import { Component, OnInit, Output, Inject, EventEmitter, Input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Language, TranslatorLanguage } from '../../models';
import { LanguageService } from '../../services/language.service';
import { Proficiency } from '../../models/proficiency';
import { UserService } from '../../services/user.service';

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
        private languageService: LanguageService,
        private userService: UserService
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
                    this.newTranslatorLanguages.push(this.translatorLanguages[index])
                }
                else{
                    this.newTranslatorLanguages.push({
                        proficiency: null,
                        language: lang
                    });
                }
            });
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
    }

    submit(){
        const filteredTranslatorLanguages = this.newTranslatorLanguages.filter(tl => tl.proficiency != null);
        this.languageService.SetCurrentUserLaguage(filteredTranslatorLanguages).subscribe(() => {
            this.dialogRef.close();
        });
    }
    close(){
        this.dialogRef.close();
    }

    getStringProficiency(prof: Proficiency){
        return Proficiency[prof];
    }
    divideCamelCase(str: string){
        return str.replace(/([a-z](?=[A-Z]))/g, '$1 ');
    }
}
