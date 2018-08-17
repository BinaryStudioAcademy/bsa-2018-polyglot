import { Component, OnInit, Output, Inject, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '../../../../node_modules/@angular/material';

@Component({
  selector: 'app-team-assign',
  templateUrl: './team-assign.component.html',
  styleUrls: ['./team-assign.component.sass']
})
export class TeamAssignComponent implements OnInit {

  @Output() onAssign = new EventEmitter<Array<number>>(true);
  teams: Array<any> = [];
  public selectedTeams: Array<any> = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<TeamAssignComponent>
  ) {
    debugger;
    if(data && data.teams)
      this.teams = data.teams;
   }

  ngOnInit() {
  }

  change($event, team){
debugger;
    let inArray = this.selectedTeams.find(t => t.id === team.id);

    if($event.checked && !inArray)
      this.selectedTeams.push(team);
    else if(!$event.checked && inArray){
      this.selectedTeams = this.selectedTeams.filter(t => t.id != team.id);
    }
  }

  assign(){
    debugger;
    if(this.selectedTeams.length > 0)
    {
      this.onAssign.emit(this.selectedTeams);
    }
    this.dialogRef.close();
  }

}
