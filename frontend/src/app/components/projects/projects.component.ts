import { Component, OnInit } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectService } from '../../services/project.service';

import { MatDialog } from '../../../../node_modules/@angular/material';
import { ProjectMessageComponent } from '../../dialogs/project-message/project-message.component';

// to delete manager and user
import { Manager } from '../../models/manager';
import { UserProfile } from '../../models/user-profile';
import { forEach } from '@angular/router/src/utils/collection';




@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.sass']
})
export class ProjectsComponent implements OnInit {

  constructor(private projectService: ProjectService,public dialog: MatDialog) { }
  
  cards : Project[];

  IsLoad : boolean = true;

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
  
  this.projectService.getAll().subscribe(pr => {this.cards = pr;
    if(this.cards.length === 0){
      setTimeout(() => this.openDialog())
      }
      this.IsLoad = false;
      console.log(this.cards);
  });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ProjectMessageComponent, {
    });
  }


  }

