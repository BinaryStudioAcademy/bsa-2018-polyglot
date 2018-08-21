import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { LanguageService } from '../../../services/language.service';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';
import { DeleteProjectLanguageComponent } from '../../../dialogs/delete-project-language/delete-project-language.component';
import { MatDialog } from '@angular/material';
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
  public IsLoading: any = {};

  constructor(
    private projectService: ProjectService, 
    private langService: LanguageService,
    private snotifyService: SnotifyService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {

   //this.langs = [
   //  {
   //    id: 1,
   //    name: 'English',
   //    progress: 25,
   //    translatedCount: 140
   //  },
   //  {
   //    id: 2,
   //    name: 'Russian',
   //    progress: 3,
   //    translatedCount: 14
   //  },
   //  {
   //    id: 3,
   //    name: 'Spanish',
   //    progress: 67,
   //    translatedCount: 863
   //  },
   //  {
   //    id: 4,
   //    name: 'German',
   //    progress: 100,
   //    translatedCount: 32
   //  },
   //  {
   //    id: 5,
   //    name: 'Polish',
   //    progress: 89,
   //    translatedCount: 340
   //  }
   //];
   //this.langs.sort(this.compareProgress);

    this.projectService.getProjectLanguages(this.projectId)
        .subscribe(langs => {
          this.IsLoad = false;
          this.langs = langs;
          this.langs.sort(this.compareProgress);
        },
        err => {
          this.IsLoad = false;
        });
  }

  selectNew(){
    this.IsLangLoad = true;
    const thisLangs = this.langs;

    this.langService.getAll()
    .subscribe(langs =>{
      let langsToSelect = langs.filter(function(language) {
        let l = thisLangs.find(t => t.id === language.id);
        if(l)
          return language.id !== l.id;
        return true;
      });
      
      this.IsLangLoad = false;
      if(langsToSelect.length < 1){
        this.snotifyService.info("No languages available to select, all of them already added", "Sorry!");
        return;
      }
      let dialogRef = this.dialog.open(SelectProjectLanguageComponent, {
          
        data: {
          langs: langsToSelect
        }
      });

      dialogRef.componentInstance.onSelect.subscribe((data) => {
        if(data)
        {
          this.IsLoad = true;
            this.projectService.addLanguagesToProject(this.projectId, data.map(l => l.id))
              .subscribe((project) => {

                if(project){
                  //this.langs.push(data);
                  
                  Array.prototype.push.apply(this.langs, data.filter(function(language) {
                    let l = thisLangs.find(t => t.id === language.id);
                    if(l)
                      return language.id !== l.id;
                    return true;
                  }));
                  this.langs.sort(this.compareProgress);
                  this.IsLoad = false;

                }
                else
                {
                  this.snotifyService.error("An error occurred while adding languages to project, please try again", "Error!");
                }
                
              },
              err => {
                this.snotifyService.error("An error occurred while adding languages to project, please try again", "Error!");
                console.log('err', err);
                
              })
        }
        else
        {
          this.snotifyService.error("An error occurred while adding languages to project, please try again", "Error!");
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
    this.IsLoading[languageId] = true;
    this.projectService.deleteProjectLanguage(this.projectId, languageId)
    .subscribe(() => {
      this.IsLoading[languageId] = false;
      this.langs = this.langs.filter(l => l.id != languageId);
      this.langs.sort(this.compareProgress);
      setTimeout(() => {
        this.snotifyService.success("Language removed", "Success!");
      }, 100);
    },
    err => {
      this.IsLoading[languageId] = false;
      this.snotifyService.error("Language wasn`t removed", "Error!");
      console.log('err', err);
      
    }
  );
  }

  compareProgress(a,b) {
    if (a.progress < b.progress)
      return -1;
    if (a.progress > b.progress)
      return 1;
    return 0;
  }

  computeStrings(translationsCount, progress): number{
    if(progress < 1)
      return 0;
    else
      return translationsCount / progress * 100;
  }
  
}