import { Component, OnInit } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectService } from '../../services/project.service';
import { MatDialog } from '../../../../node_modules/@angular/material';
import { ProjectMessageComponent } from '../../dialogs/project-message/project-message.component';



@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.sass']
})
export class ProjectsComponent implements OnInit {


  constructor(private projectService: ProjectService,public dialog: MatDialog) { }
  
  cards : Project[];

  ngOnInit() {
  this.projectService.getAll().subscribe(pr => this.cards = pr);
  if(this.cards.length == 0){
    setTimeout(() => this.openDialog())
    }
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ProjectMessageComponent, {
    });
  }

}
