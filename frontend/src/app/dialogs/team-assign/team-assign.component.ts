import { Component, OnInit, Output, Inject, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '../../../../node_modules/@angular/material';

@Component({
  selector: 'app-team-assign',
  templateUrl: './team-assign.component.html',
  styleUrls: ['./team-assign.component.sass']
})
export class TeamAssignComponent implements OnInit {

  @Output() onAssign = new EventEmitter<any>(true);
  teams: any = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public inputTeams: any,
    public dialogRef: MatDialogRef<TeamAssignComponent>
  ) {
    if(inputTeams)
      this.teams = inputTeams;
   }

  ngOnInit() {
  }

  assign(){

  }

}
