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
    { text: 'Project1', progress: 40 },
    { text: 'Project2', progress: 30 },
    { text: 'Project3', progress: 29 },
    { text: 'Project4', progress: 85 },
    { text: 'Project5', progress: 100 },
    { text: 'Project6', progress: 15 },
    { text: 'Project7', progress: 26 },
  ];


  constructor() { }

  ngOnInit() {
  }

}
