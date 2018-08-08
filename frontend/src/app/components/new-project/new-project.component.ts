import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { Project } from '../../models/project';
import { Language } from '../../models/language';
import { TypeTechnology } from '../../models/type-technology.enum';
import { ProjectService } from '../../services/project.service';
import { LanguageService } from '../../services/language.service';

@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html',
  styleUrls: ['./new-project.component.sass']
})

export class NewProjectComponent implements OnInit {

  constructor(private fb: FormBuilder, private projectService: ProjectService,
    private languageService: LanguageService) {

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

    // this.languageService.getAll()
    //   .subscribe(
    //   (d)=> {
    //     this.languages = d;
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
        name: [ '', [Validators.required]],
        description: [ '', []],
        technology: [ '', []],
        mainLanguage: [ '', []],
      });
  }

  saveChanges(project: Project): void{
    debugger
    project.createdOn = new Date(Date.now()); 
    //Save current manager
    this.projectService.create(project)
    .subscribe(
      (d)=> {
        console.log(d);
      },
      err => {
        console.log('err', err);
      }
    );
  }
}
