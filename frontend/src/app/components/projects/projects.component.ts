import { Component, OnInit, OnDestroy } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectService } from '../../services/project.service';
import { MatDialog } from '@angular/material';
import { ProjectMessageComponent } from '../../dialogs/project-message/project-message.component';
import {AppStateService} from '../../services/app-state.service';


// to delete manager and user
import { UserProfile } from '../../models/user-profile';

import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import { UserService } from '../../services/user.service';


@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.sass']
})
export class ProjectsComponent implements OnInit,OnDestroy {

  public state: string;
  public checked = true;
  
  constructor(
    private userService: UserService,
    private appStateService: AppStateService,
    private projectService: ProjectService,
    public dialog: MatDialog,
    private snotifyService: SnotifyService) { }
  
  public cards: Project[];
  IsLoad : boolean = true;
  OnPage : boolean;
   
  manager: UserProfile =  this.userService.getCurrrentUser();

  ngOnInit() {
  this.OnPage = true;  
 
   if(this.appStateService.Layout === null || this.appStateService.Layout === 'card') {
     this.checked = true;
     this.appStateService.Layout = 'card';
   } else {
     this.checked = false;
   }
    
  debugger;

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

  changeLayout(){
    debugger;   
   if(this.checked){
     this.appStateService.Layout = 'row';
     this.checked = false;
   } else {
     this.appStateService.Layout = 'card';
     this.checked = true;
   }
  }

}
