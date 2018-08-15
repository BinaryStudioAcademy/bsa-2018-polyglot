import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ProjectService } from '../../services/project.service';
import { Project } from '../../models';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.sass']
})
export class ProjectDetailsComponent implements OnInit {

  private routeSub: Subscription;
  public project: Project;

  constructor(private activatedRoute: ActivatedRoute,
              private dataProvider: ProjectService) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      this.getProjById(params.projectId);
      
    });
  }

  getProjById(id: number){
    this.dataProvider.getById(id).subscribe(proj =>{
      this.project = proj;
    });
  }
}
