import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { LanguageService } from '../../../services/language.service';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import { DeleteProjectLanguageComponent } from '../../../dialogs/delete-project-language/delete-project-language.component';
import { MatDialog } from '../../../../../node_modules/@angular/material';
import { SelectProjectLanguageComponent } from '../../../dialogs/select-project-language/select-project-language.component';

@Component({
  selector: 'app-languages',
  templateUrl: './languages.component.html',
  styleUrls: ['./languages.component.sass']
})
export class LanguagesComponent implements OnInit {

  @Input() projectId: number;
  public langs = [];
  public IsLoad: boolean = true;
  public IsLangLoad: boolean = false;

  constructor(
    private projectService: ProjectService, 
    private langService: LanguageService,
    private snotifyService: SnotifyService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.projectService.getProjectLanguages(this.projectId)
        .subscribe(langs => {
          this.IsLoad = false;
          this.langs = langs;
        },
        err => {
          this.IsLoad = false;
        });
  }

  selectNew(){
    this.IsLangLoad = true;
    this.langService.getAll()
    .subscribe(langs =>{
      this.IsLangLoad = false;
      let dialogRef = this.dialog.open(SelectProjectLanguageComponent, {
        data: {
          langs: langs
        }
      });

      dialogRef.componentInstance.onSelect.subscribe((data) => {
        if(data)
        {
          data.forEach(language => {
          this.IsLoad = true;
            this.projectService.addLanguageToProject(this.projectId, language.id)
              .subscribe((project) => {
                this.langs.push(language);
                this.IsLoad = false;
              })
          });
        }
        else
        {
          this.snotifyService.error(data.message, "Error!");
        }
      });

      dialogRef.afterClosed().subscribe(()=>{
        dialogRef.componentInstance.onSelect.unsubscribe();
      });

    })
    
  }

  onDeleteLanguage(languageId: number){
    if(this.langs.filter(l => l.id === languageId)[0].translationsCount > 0)
      {
        const dialogRef = this.dialog.open(DeleteProjectLanguageComponent, {
          data: {
            languageName: this.langs.filter(l => l.id === languageId)[0].name,
            translationsCount: this.langs.filter(l => l.id === languageId)[0].translationsCount
          }
        });

        dialogRef.componentInstance.onConfirmDelete.subscribe((data) => {
          if(data && data.state)
          {
            this.snotifyService.info(data.message, "Deletion confirmed.");
            this.deleteLanguage(languageId)
          }
          else
          {
            this.snotifyService.error(data.message, "Error!");
          }
        });

        dialogRef.afterClosed().subscribe(()=>{
          dialogRef.componentInstance.onConfirmDelete.unsubscribe();
        });
      }
    else{
      this.deleteLanguage(languageId);
    }
  }

  deleteLanguage(languageId: number){
    this.projectService.deleteProjectLanguage(this.projectId, languageId)
    .subscribe(() => {

      this.langs = this.langs.filter(l => l.id != languageId);
      setTimeout(() => {
        this.snotifyService.success("Language removed", "Success!");
      }, 100);
    },
    err => {
      this.snotifyService.error("Language wasn`t removed", "Error!");
      console.log('err', err);
      
    }
  );
  }
  
}