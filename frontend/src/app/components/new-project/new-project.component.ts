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
    this.languages = [
    {
      code: 'en',
      id: undefined,
      name: 'English'
    },
    {
      code: 'fr',
      id: undefined,
      name: 'French'
    },{
      code: 'pl',
      id: undefined,
      name: 'Polish'
    }, ];

    //if table with languages have values, it will uncommoment
    // this.languageService.getAll()
    //   .subscribe(
    //   (d)=> {
    //     this.languages = d;
    //     this.createProjectForm();
    //     console.log(d);
    //   },
    //   err => {
    //     console.log('err', err);
    //   }
    // );  
  }

  project: Project;
  projectForm: FormGroup;
  languages: Array<Language>;
  
  // public errorMessages = {
  //   name: 'This field is required'
  // };

  createProjectForm(): void {
    
      this.projectForm = this.fb.group({
        name: [ '', [Validators.required, Validators.minLength(4), Validators.pattern('[a-zA-Z ]*')]],
        description: [ '', [Validators.pattern('[a-zA-Z ]*'), Validators.maxLength(500)]],
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
