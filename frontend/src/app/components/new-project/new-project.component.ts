import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import {FormBuilder, FormGroup, Validators, FormControl} from '@angular/forms';
import { Project } from '../../models/project';
import { Language } from '../../models/language';
import { TypeTechnology } from '../../models/type-technology.enum';
import { ProjectService } from '../../services/project.service';
import { LanguageService } from '../../services/language.service';
import { Router } from '@angular/router';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import { debounce } from 'rxjs/operators';

@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html',
  styleUrls: ['./new-project.component.sass']
})

export class NewProjectComponent implements OnInit {

  constructor(private fb: FormBuilder, private projectService: ProjectService,
    private languageService: LanguageService, 
    private router: Router,
    private snotifyService: SnotifyService,) {

  }
  
  ngOnInit() {
    this.languageService.getAll()
      .subscribe(
      (d: Language[])=> {
        this.languages = d.map(x => Object.assign({}, x));
      },
      err => {
        console.log('err', err);
      }
    );  
  }

  receiveImage($event){
      this.projectImage = $event[0];
  }

  projectImage: File;
  project: Project;
  projectForm: FormGroup = this.fb.group({
    name: [ '', [Validators.required, Validators.minLength(4)]],
    description: [ '', [Validators.maxLength(500)]],
    technology: [ '', [Validators.required]],
    mainLanguage: [ '', [Validators.required]],
  });
  languages: Language[];
  
  saveChanges(project: Project): void{
    project.createdOn = new Date(Date.now());
    let formData = new FormData();
    if(this.projectImage)
      formData.append("image", this.projectImage);

      console.log(project);
    formData.append("project", JSON.stringify(project));

    this.projectService.create(formData)
    .subscribe(
      (d)=> {
        console.log(d);
        this.router.navigate(['../']);
        setTimeout(() => {
          this.snotifyService.success("Project created", "Success!");
        }, 100);
        
      },
      err => {
        this.snotifyService.error("Project wasn`t created", "Error!");
        console.log('err', err);
        
      }
    );
  }

  getAllTechnologies() {
    return Object.keys(TypeTechnology).filter(
      (type) => isNaN(<any>type) && type !== 'values'
    );
  }

  getLanguages(){
    return this.languages.map(l => l.name);
  }


  get name() {
    return this.projectForm.get('name');
  }

  get technology() {
    return this.projectForm.get('technology');
  }

  get mainLanguage() {
    return this.projectForm.get('mainLanguage');
  }

  get description() {
    return this.projectForm.get('description');
  }
}