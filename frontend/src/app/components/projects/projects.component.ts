import { Component, OnInit } from '@angular/core';


export interface Project {

  text: string;
  progress: number;
}

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.sass']
})
export class ProjectsComponent implements OnInit {

  cards: Project[] = [
    { text: 'Batman', progress: 40 },
    { text: 'Superman', progress: 30 },
    { text: 'Angular', progress: 29 },
    { text: 'Justice', progress: 85 },
    { text: 'Valkiriya', progress: 100 }

  ];


  constructor() { }

  ngOnInit() {
  }

}
