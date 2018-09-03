import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Team } from '../../models';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from '../../services/teams.service';

@Component({
  selector: 'app-team-details',
  templateUrl: './team-details.component.html',
  styleUrls: ['./team-details.component.sass']
})
export class TeamDetailsComponent implements OnInit {

  private routeSub: Subscription;
  public team: Team;

  constructor(
    private activatedRoute: ActivatedRoute,
    private dataProvider: TeamService
  ) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      this.getTeamById(params.teamId)
    });
  }

  getTeamById(id: number) {
    this.dataProvider.getTeam(id).subscribe(team => {
      this.team = team;
    });
  }
}
