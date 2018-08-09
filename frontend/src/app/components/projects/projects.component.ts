import { Component, OnInit } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectService } from '../../services/project.service';



@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.sass']
})
export class ProjectsComponent implements OnInit {


  constructor(private projectService: ProjectService) { }
  
  cards : Project[];

  ngOnInit() {
	this.projectService.getAll().subscribe(pr => this.cards = pr);
  }

}
