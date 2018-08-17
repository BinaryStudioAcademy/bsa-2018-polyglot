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
  assignedTeam: any;
  public IsLoad: boolean = true;
  public IsTeamAssigned: boolean = false;
  public IsTeamsLoading: boolean = false;

  constructor(
    private projectService: ProjectService,
    private teamsService: TeamService,
    private snotifyService: SnotifyService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.projectService.getProjectTeam(this.projectId)
        .subscribe(assignedTeam => {
          if(assignedTeam)
            {
              this.assignedTeam = assignedTeam;
              this.IsTeamAssigned = true;
            }else
            {
              /// FIRE THE ASSIGN TEAM DIALOG
            }
          this.IsLoad = false;
        },
        err => {
          this.IsLoad = false;
        });
  }

  assignTeam(){
    this.IsTeamsLoading = true;

    debugger;
    this.teamsService.getAll()
      .subscribe(teams => {

        if(!teams || teams.length < 1){
          this.snotifyService.error("No teams found!", "Error!");
          this.IsTeamsLoading = false;
          return;
        }

        debugger;
        this.IsTeamsLoading = false;
        let dialogRef = this.dialog.open(TeamAssignComponent, {
        data: {
          teams: teams
        }
      });

      dialogRef.componentInstance.onAssign.subscribe((selectedTeam) => {
        debugger;
        if(selectedTeam)
        {
          
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
      this.IsTeamsLoading = false;
      this.snotifyService.error("Shit happens (TODO: get the error message)", "Error!");
      console.log('err', err);
      
    });
    
  }
}
