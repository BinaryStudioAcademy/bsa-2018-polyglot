import { Component, OnInit } from '@angular/core';
import { TeamService } from "../../services/teams.service";
import { Team } from '../../models';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {

  IsLoad : boolean = true;
  managerId: number = 1;
  teams: any;

  constructor(private teamsService: TeamService) { }

  ngOnInit() {
    this.getAllTeams();
  }

  getAllTeams(){
    this.teamsService.getAllTeams()
      .subscribe((teams) => {
        
        this.teams = teams;
        let a = teams
        this.IsLoad = false;
      })
  }

}
