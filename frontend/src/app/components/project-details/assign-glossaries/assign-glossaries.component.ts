import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { Glossary, Project } from '../../../models';
import { GlossaryService } from '../../../services/glossary.service';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-assign-glossaries',
  templateUrl: './assign-glossaries.component.html',
  styleUrls: ['./assign-glossaries.component.sass']
})
export class AssignGlossariesComponent implements OnInit {

  @Input() projectId: number;
  public AssignedGlossaries: Glossary[] = [];
  public AllGlossaries: Glossary[] = [];
  public Project: Project;
  displayedColumns: string[] = ['name','originLanguage', 'action_btn'];
  assignedDataSource : MatTableDataSource<Glossary>;
  allDataSource : MatTableDataSource<Glossary>;

  constructor(
    private projectService : ProjectService,
    private glossariesService : GlossaryService
  ) { }

  ngOnInit() {
    this.refresh();
  }

  refresh(){
    this.glossariesService.getAll().subscribe(data =>{
      this.AllGlossaries = data;
      this.allDataSource = new MatTableDataSource(this.AllGlossaries);
    });
    this.projectService.getById(this.projectId).subscribe(data =>{
      this.Project = data;
      this.Project.projectGlossaries.forEach(i => {
        this.AssignedGlossaries.push(i.glossary);
      });
      this.assignedDataSource = new MatTableDataSource(this.AssignedGlossaries);
    })
  }

  isAssigned(item : Glossary) : boolean{
    let res = false;
    this.AssignedGlossaries.forEach(i =>{
      if(i.id === item.id){
        res = true;
      }
    });
    return res;
  }

  onAction(item : Glossary){
    if(this.isAssigned(item)){
      this.AssignedGlossaries.splice(this.AssignedGlossaries.indexOf(item), 1);
      this.projectService.dismissProjectGlossary(this.projectId, item.id);
    } else {
      this.AssignedGlossaries.push(item);
      this.projectService.assignGlossariesToProject(this.projectId, [item.id]);
    }
    this.refresh();
  }

}
