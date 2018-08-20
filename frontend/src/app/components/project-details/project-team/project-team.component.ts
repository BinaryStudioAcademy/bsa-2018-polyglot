import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '../../../../../node_modules/@angular/material';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';
import { ProjectService } from '../../../services/project.service';
import { TeamService } from '../../../services/teams.service';
import { TeamAssignComponent } from '../../../dialogs/team-assign/team-assign.component';


@Component({
  selector: 'app-project-team',
  templateUrl: './project-team.component.html',
  styleUrls: ['./project-team.component.sass']
})
export class ProjectTeamComponent implements OnInit {

  @Input() projectId: number;
  public assignedTeams: Array<any> = [];
  public IsLoad: boolean = true;

  constructor(
    private projectService: ProjectService,
    private teamsService: TeamService,
    private snotifyService: SnotifyService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
   // debugger;
    this.projectService.getProjectTeams(this.projectId)
        .subscribe(assignedTeams => {
         // debugger;
          if(assignedTeams && assignedTeams.length > 0)
            {
              this.assignedTeams = assignedTeams;
            }else
            {
              this.assignedTeams = [];
              ///TODO: FIRE THE ASSIGN TEAM DIALOG
            }
          this.IsLoad = false;
        },
        err => {
          this.snotifyService.error("An error occurred while loading teams assigned to this project, please try again later", "Error!");
          this.IsLoad = false;
        });
  }

  assignTeam(){

    if(this.IsLoad)
      return;

    this.IsLoad = true;
   // debugger;
    this.teamsService.getAllTeams()
      .subscribe(teams => {
      //  debugger;
        if(!teams || teams.length < 1){
          this.snotifyService.error("No teams found!", "Error!");
          this.IsLoad = false;
          return;
        }

     //   debugger;
        const thisTeams = this.assignedTeams;
        let avaibleTeams = teams.filter(function(team) {
          let t = thisTeams.find(t => t.id === team.id);
          if(t)
            return team.id !== t.id;
          return true;
        })

        this.IsLoad = false;

        if(!avaibleTeams || avaibleTeams.length < 1)
        {
          this.snotifyService.error("No more teams avaible to assign!", "Error!");
          this.IsLoad = false;
          return;
        }        
        let dialogRef = this.dialog.open(TeamAssignComponent, {
          hasBackdrop: true,
          width: '640px',
        data: {
          teams: avaibleTeams
        },
      });

      dialogRef.componentInstance.onAssign.subscribe((selectedTeams : Array<any>) => {
        this.IsLoad = true;
        debugger;
        if(selectedTeams && selectedTeams.length > 0)
        {
          this.projectService.assignTeamsToProject(this.projectId, selectedTeams.map(t => t.id))
            .subscribe(responce => {
              ///TODO: fire a progress notification//////////////////////////////////////////////////////////////
              debugger;
              if(responce)
              {
                Array.prototype.push.apply(this.assignedTeams, selectedTeams.filter(function(team) {
                  let t = thisTeams.find(t => t.id === team.id);
                  if(t)
                    return team.id !== t.id;
                  return true;
                }));
                this.IsLoad = false;
                this.snotifyService.success("Teams successfully assigned!", "Success!");
              }
            },
          err => {
            debugger;
            this.IsLoad = false;
            this.snotifyService.error(err, "Error!");
            console.log('err', err);
          });
        }
        else
        {
        //  this.snotifyService.error(data.message, "Error!");
        }
      });

      dialogRef.afterClosed().subscribe(()=>{
        dialogRef.componentInstance.onAssign.unsubscribe();
      });

    },
    err => {
      debugger;
      this.snotifyService.error("An error occurred while loading teams, please try again later", "Error!");
      console.log('err', err);
      
    });
    
  }

  dismissTeam(id: number){
    this.projectService.dismissProjectTeam(this.projectId, id)
      .subscribe(responce => {
        this.snotifyService.success("Team " + id + " succesfully dismissed!", "Success!");
        this.assignedTeams = this.assignedTeams.filter(t => t.id !== id);
      })
  }

  getAvatarUrl(person): String {
    if(person.avatarUrl)
      return person.avatarUrl;
    else
      return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTsrMId-b7-CLWIw6S80BQZ6Xqd7jX0rmU9S7VSv_ngPOU7NO-6Q";
  }
}
