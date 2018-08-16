import { Component, OnInit, OnDestroy } from '@angular/core';
import { Project } from '../../models/project';
import { ManagerService } from '../../services/manager.service';
import { ProjectService } from '../../services/project.service';

import { MatDialog } from '../../../../node_modules/@angular/material';
import { ProjectMessageComponent } from '../../dialogs/project-message/project-message.component';

// to delete manager and user
import { Manager } from '../../models/manager';
import { UserProfile } from '../../models/user-profile';

import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';


@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.sass']
})
export class ProjectsComponent implements OnInit,OnDestroy {
  public cards: Project[];


  constructor(
    //private managerService: ManagerService, 
    private projectService: ProjectService,
    public dialog: MatDialog,
    private snotifyService: SnotifyService) { }
  

  IsLoad : boolean = true;
  OnPage : boolean


  user: UserProfile = {
    id: 1,
    firstName: 'Bill',
    lastName: 'Gates',
    birthDate: null,
    registrationDate: null,
    country: 'string',
    city: 'string',
    region: 'string',
    postalCode: 'string',
    address: 'string',
    phone: 'string',
    avatarUrl: 'https://pbs.twimg.com/profile_images/988775660163252226/XpgonN0X_400x400.jpg'
  };
  manager: Manager = {
    id:  1,
    userProfile: this.user
  };

  ngOnInit() {
  this.OnPage = true;

  this.projectService.getAll().subscribe(pr => 
    {
      this.cards = pr;
    if(this.cards.length === 0 && this.OnPage === true){
     setTimeout(() => this.openDialog())
      }
      this.IsLoad = false;
  });
  }

  ngOnDestroy(){
    this.OnPage = false;
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ProjectMessageComponent, {
    });
  }

  delete(id: number): void{
    this.projectService.delete(id)
    .subscribe(
    //.subscribe( value => console.log(value));
      (response => {
        let projectToDelete = this.cards.find(pr => pr.id == id);
        let projectToDeleteIndex = this.cards.indexOf(projectToDelete);
        this.cards.splice(projectToDeleteIndex, 1);
        this.snotifyService.success("Project deleted", "Success!");
      }),
      err => {
        this.snotifyService.error("Project wasn`t deleted", "Error!");
        console.log('err', err);
        
      }
    );
    console.log(id)
    console.log("deleted")
   }
}

  

