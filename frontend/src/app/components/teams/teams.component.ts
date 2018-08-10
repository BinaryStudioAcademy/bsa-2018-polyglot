import { Component, OnInit } from '@angular/core';

export interface Team {

  text: string;
  rating: number;
  translators: string[];
}

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {

  teams: Team[] = [
    { text: 'Team1', rating: 40, translators: ['https://bit.ly/2KKdyuV', 'https://bit.ly/2tIuZEL'] },
    { text: 'Team2', rating: 30, translators: ['https://bit.ly/2tIuZEL', 'https://bit.ly/2tIuZEL'] },
    { text: 'Team3', rating: 29, translators: ['https://bit.ly/2tIuZEL', 'https://bit.ly/2KKdyuV'] },
    { text: 'Team4', rating: 85, translators: ['https://bit.ly/2KKdyuV', 'https://bit.ly/2KKdyuV'] },
    { text: 'Team5', rating: 100, translators: ['https://bit.ly/2tIuZEL', 'https://bit.ly/2KKdyuV'] },
  ];

  constructor() { }

  ngOnInit() {
  }

}
