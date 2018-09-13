import { Component, OnInit } from '@angular/core';
import { TeamService } from "../../services/teams.service";
import { UserService } from '../../services/user.service';

@Component({
    selector: 'app-teams',
    templateUrl: './teams.component.html',
    styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {

    isLoad: boolean = true;
    teams: any = [];
    searchQuery: string;

    constructor(private teamsService: TeamService,
        private userService: UserService) { }

    ngOnInit() {
        this.getAllTeams();
    }

    getAllTeams() {
        this.teamsService.getAllTeams()
            .subscribe((teams) => {
                this.teams = teams;
                this.isLoad = false;
            });
    }

    isCurrentUserManager() {
        return this.userService.isCurrentUserManager();
    }

    search() {
        this.searchQuery = this.searchQuery.trim();
        this.teamsService.searchTeams(this.searchQuery)
            .subscribe(t => {
                this.teams = t;
            });

    }
}
