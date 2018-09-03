import { Component, OnInit, Input } from '@angular/core';
import { TeamProject } from '../../../models/team-project';

@Component({
  selector: 'app-team-project',
  templateUrl: './team-project.component.html',
  styleUrls: ['./team-project.component.sass']
})
export class TeamProjectComponent implements OnInit {
  @Input() cards: TeamProject[];
  constructor() { }

  ngOnInit() {
  }

}
