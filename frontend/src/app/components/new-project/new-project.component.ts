import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { Project } from '../../models/project';
import { Language } from '../../models/language';
import { TypeTechnology } from '../../models/type-technology.enum';
import { ProjectService } from '../../services/project.service';
import { LanguageService } from '../../services/language.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html',
  styleUrls: ['./new-project.component.sass']
})

export class NewProjectComponent implements OnInit {

  constructor(private fb: FormBuilder, private projectService: ProjectService,
    private languageService: LanguageService, 
    private router: Router) {

  }

  
  ngOnInit() {
    this.createProjectForm();

    this.languageService.getAll()
      .subscribe(
      (d)=> {
        this.languages = d;
        this.createProjectForm();
      },
      err => {
        console.log('err', err);
      }
    );  
  }

  project: Project;
  projectForm: FormGroup;
  languages: Array<Language>;

  createProjectForm(): void {
    
      this.projectForm = this.fb.group({
        name: [ '', [Validators.required, Validators.minLength(4)]],
        description: [ '', [Validators.maxLength(500)]],
        technology: [ '', [Validators.required]],
        mainLanguage: [ '', [Validators.required]],
      });
  }

  saveChanges(project: Project): void{
    project.createdOn = new Date(Date.now()); 
    //Save current manager
    this.projectService.create(project)
    .subscribe(
      (d)=> {
        console.log(d);
        this.router.navigate(['../']);
      },
      err => {
        console.log('err', err);
      }
    );
  }

  values() {
    return Object.keys(TypeTechnology).filter(
      (type) => isNaN(<any>type) && type !== 'values'
    );
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
