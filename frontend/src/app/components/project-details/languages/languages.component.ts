import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { LanguageService } from '../../../services/language.service';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';

@Component({
  selector: 'app-languages',
  templateUrl: './languages.component.html',
  styleUrls: ['./languages.component.sass']
})
export class LanguagesComponent implements OnInit {

  @Input() projectId: number;
  public langs;

  constructor(
    private projectService: ProjectService, 
    private langService: LanguageService,
    private snotifyService: SnotifyService) { }

  ngOnInit() {
    this.projectService.getProjectLanguages(this.projectId)
        .subscribe(langs => {
          this.langs = langs;
        })
  }

  selectNew(){

  }

  addNewString(){
  //  let dialogRef = this.dialog.open(StringDialogComponent, {
  //    data: {
    //    projectId: this.project.id
  //    }
  //    });
  //    dialogRef.componentInstance.onAddString.subscribe((result) => {
  //      if(result)
    //      this.keys.push(result);
    //      this.selectedKey = result;
    //      let keyId = this.keys[0].id;   
    //      this.router.navigate([this.currentPath, keyId]);
    //      this.isEmpty = false;
  //    })
  //    dialogRef.afterClosed().subscribe(()=>{
  //      dialogRef.componentInstance.onAddString.unsubscribe();
  //    });
  }

  onDeleteLanguage(id: number){
    this.langService.delete(id)
    .subscribe(() => {

      this.langs = this.langs.filter(l => l.id != id);
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