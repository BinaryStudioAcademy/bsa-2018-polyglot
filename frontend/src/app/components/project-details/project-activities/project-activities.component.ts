import { Component, OnInit, Input } from '@angular/core';
import { Project } from '../../../models';
import { ProjectService } from '../../../services/project.service';

@Component({
  selector: 'app-project-activities',
  templateUrl: './project-activities.component.html',
  styleUrls: ['./project-activities.component.sass']
})
export class ProjectActivitiesComponent implements OnInit {

  @Input() project: Project;
  constructor(private projectService: ProjectService) { }

  ngOnInit() {
    this.projectService.getProjectActivitiesById(this.project.id).subscribe(data =>{
      console.log(data);
    });
  }



}
