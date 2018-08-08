import { Component, OnInit } from '@angular/core';

export interface Team {

  text: string;
  rating: number;
}

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {
  
  teams: Team[] = [
    { text: 'Team1', rating: 40 },
    { text: 'Team2', rating: 30 },
    { text: 'Team3', rating: 29 },
    { text: 'Team4', rating: 85 },
    { text: 'Team5', rating: 100 },
  
  ];

  constructor() { }

  ngOnInit() {
  }

}
