import { Component, OnInit } from '@angular/core';
import { TeamService } from "../../services/teams.service";

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
    this.teamsService.getAll()
      .subscribe(teams => {
        debugger;
        this.teams = teams;
        let a = teams.persons;
        this.IsLoad = false;
      })
  }

}
