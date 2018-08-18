import { Component, OnInit, OnDestroy } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectService } from '../../services/project.service';

import { MatDialog } from '@angular/material';
import { ProjectMessageComponent } from '../../dialogs/project-message/project-message.component';

// to delete manager and user
import { UserProfile } from '../../models/user-profile';

import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';


@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.sass']
})
export class ProjectsComponent implements OnInit,OnDestroy {
  
  constructor(
    //private managerService: ManagerService, 
    private projectService: ProjectService,
    public dialog: MatDialog,
    private snotifyService: SnotifyService) { }
  
  public cards: Project[];
  IsLoad : boolean = true;
  OnPage : boolean;
  
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
  manager: UserProfile =  this.user;

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
}
