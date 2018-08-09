import { Component, OnInit } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectService } from '../../services/project.service';


<<<<<<< HEAD
export interface Project {
  id: number,
  text: string;
  progress: number;
}
=======
>>>>>>> 8251e889947afb0da4b5330e7ee01f0f076f0b59

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.sass']
})
export class ProjectsComponent implements OnInit {

<<<<<<< HEAD
  cards: Project[] = [
    { id: 1, text: 'Project1', progress: 40 },
    { id: 2, text: 'Project2', progress: 30 },
    { id: 3, text: 'Project3', progress: 29 },
    { id: 4, text: 'Project4', progress: 85 },
    { id: 5, text: 'Project5', progress: 100 },
    { id: 6, text: 'Project6', progress: 15 },
    { id: 7, text: 'Project7', progress: 26 },
  ];

=======
>>>>>>> 8251e889947afb0da4b5330e7ee01f0f076f0b59

  constructor(private projectService: ProjectService) { }
  
  cards : Project[];

  ngOnInit() {
	this.projectService.getAll().subscribe(pr => this.cards = pr);
  }

}
