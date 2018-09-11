import { Component, OnInit, Input } from '@angular/core';
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
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';
import { AppStateService } from '../../services/app-state.service';

@Component({
  selector: 'app-manager-profile',
  templateUrl: './manager-profile.component.html',
  styleUrls: ['./manager-profile.component.sass']
})
export class ManagerProfileComponent implements OnInit {

  fullName : string;
  @Input() public manager : UserProfile
  projects : Project[]
  teams : Team[]
  description: string = "Are you sure you want to leave the team?";
  btnOkText: string = "Yes";
  btnCancelText: string = "No";
  answer: boolean;
  isLoad: boolean = true;
  isNoProjects: boolean;

  constructor(
    public dialog: MatDialog,
    private router: Router,
    private userService: UserService,
    private projectService: ProjectService,
    private teamService: TeamService,
    private snotifyService: SnotifyService,
    private appStateService: AppStateService) {
  }


  ngOnInit(): void {

    this.projectService.getAll().subscribe(pr => {
        this.projects = pr;
        this.isNoProjects = this.projects.length === 0;
        this.isLoad = false;
    });
    this.userService.getUserTeams(this.manager.id).subscribe(t => {
      this.teams = t;
    });
  }

  isOwnersProfile(){
    return this.manager.id == this.userService.getCurrentUser().id;
  }

  editPhoto(){
    if(this.isOwnersProfile()){
      const dialogRef = this.dialog.open(CropperComponent, {
        data: {imageUrl: this.manager.avatarUrl}
      });
      dialogRef.afterClosed().subscribe(result => {
          if (dialogRef.componentInstance.cropedImageBlob){
              let formData = new FormData();
              formData.append("image", dialogRef.componentInstance.cropedImageBlob);
              this.userService.updatePhoto(formData).subscribe(
                  (d) => {
                      setTimeout(() => {
                          this.snotifyService.success("Photo updated.", "Success!");
                          }, 100);
                          this.manager = d;
                          this.appStateService.currentDatabaseUser = d;
                      },
                      err => {
                          this.snotifyService.error("Photo failed to update!", "Error!");
                      }
                  );
              }
          }
      );
    }
  }

  leaveTeam(team: Team) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '500px',
      data: {description: this.description, btnOkText: this.btnOkText, btnCancelText: this.btnCancelText, answer: this.answer}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (dialogRef.componentInstance.data.answer) {
        let translatorId = team.teamTranslators.find(translator => {return translator.userId === this.manager.id}).id;
        this.teamService.deletedTeamTranslators([translatorId]).subscribe(
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
    );
  }
}
