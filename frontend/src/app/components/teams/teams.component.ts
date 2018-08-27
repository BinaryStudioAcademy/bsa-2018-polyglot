import { Component, OnInit } from '@angular/core';
import { TeamService } from "../../services/teams.service";
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {

  IsLoad : boolean = true;
  teams: any;

  constructor(private teamsService: TeamService,
              private userService: UserService) { }

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

  isCurrentUserManager(){
    return this.userService.isCurrentUserManager();
  }

}
