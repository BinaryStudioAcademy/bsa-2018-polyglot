import { Component, OnInit } from '@angular/core';
import { ManagerService } from "../../services/manager.service";
import { Team } from '../../models';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {

  IsLoad : boolean = true;
  managerId: number = 1;
  teams: Team[];

  constructor(private managerService: ManagerService) { }

  ngOnInit() {
    this.getAllTeams(this.managerId);
  }

  getAllTeams(id: number){
    this.managerService.getManagerTeams(id)
      .subscribe(teams => {
        debugger;
        this.teams = teams;
        this.IsLoad = false;
      })
  }

}
