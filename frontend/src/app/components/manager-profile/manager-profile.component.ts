import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CropperComponent } from '../../dialogs/cropper-dialog/cropper.component';
import { Project } from '../../models/project';
import { UserProfile } from '../../models/user-profile';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { ProjectService } from '../../services/project.service';
import { TeamService } from '../../services/teams.service';
import { Team } from '../../models';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';

@Component({
  selector: 'app-manager-profile',
  templateUrl: './manager-profile.component.html',
  styleUrls: ['./manager-profile.component.sass']
})
export class ManagerProfileComponent implements OnInit {
  
  fullName : string;
  manager : UserProfile
  projects : Project[]
  teams : Team[]

  constructor(
    public dialog: MatDialog, 
    private router: Router, 
    private userService: UserService,
    private projectService: ProjectService,
    private teamService: TeamService,
    private snotifyService: SnotifyService) {
  }
  

  ngOnInit(): void {
    this.manager = this.userService.getCurrrentUser();

    this.projectService.getAll().subscribe(pr => {
        this.projects = pr;
    });
    this.userService.getUserTeams(this.manager.id).subscribe(t => {
      this.teams = t;
    });
  }

  editPhoto(){
    this.dialog.open(CropperComponent, {
      data: {imageUrl: this.manager.avatarUrl}
    });
  }

  leaveTeam(team: Team)
  {
    team.teamTranslators = team.teamTranslators.filter(tt => tt.userId !== this.manager.id);
    this.teamService.update(team.id, team).subscribe(
      (d) => {
        setTimeout(() => {
          this.snotifyService.success("Left team", "Success!");
        }, 100);
        this.teams = this.teams.filter(t => t.id !== team.id);
      },
      err => {
        this.snotifyService.error("Team wasn`t left", "Error!");
      }
    );
  }
}