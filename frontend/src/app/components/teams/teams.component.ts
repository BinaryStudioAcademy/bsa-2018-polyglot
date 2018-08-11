import { Component, OnInit } from '@angular/core';

export interface Team {

  i: number;
}

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {

  teams: Team[] = [
   {i: 1},
   {i: 2},
   {i: 3},
   {i: 4},
   {i: 5},
  ];

  constructor() { }

  ngOnInit() {
  }

}
