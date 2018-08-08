import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { Project } from '../../models/project';
import { Language } from '../../models/language';
import { TypeTechnology } from '../../models/type-technology.enum';
import { ProjectService } from '../../services/project.service';

@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html',
  styleUrls: ['./new-project.component.sass']
})

export class NewProjectComponent implements OnInit {

  constructor(private fb: FormBuilder,private projectService: ProjectService) {

  }

  
  ngOnInit() {
    this.createProjectForm();
    this.languages = [
    {
      code: 'en',
      id: 1,
      name: 'English'
    },
    {
      code: 'fr',
      id: 2,
      name: 'France'
    },{
      code: 'pl',
      id: 3,
      name: 'Polish'
    }, ];

    //languages = getAll();
  
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
    this.projectService.createEntity(project);
    //Save current manager
  }
}
