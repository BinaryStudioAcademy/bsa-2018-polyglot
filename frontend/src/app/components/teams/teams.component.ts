import { Component, OnInit } from '@angular/core';
import { TeamService } from "../../services/teams.service";

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {

  IsLoad : boolean = true;
  teams: any;

  constructor(private teamsService: TeamService) { }

  ngOnInit() {
    this.getAllTeams();
  }

  getAllTeams(){
    this.teamsService.getAllTeams()
      .subscribe((teams) => {       
        this.teams = teams;
        console.log(teams)
        this.IsLoad = false;
      })
  }

}
