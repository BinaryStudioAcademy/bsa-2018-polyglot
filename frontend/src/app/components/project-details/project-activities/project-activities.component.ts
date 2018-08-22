import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { Activity } from '../../../models/activity';

@Component({
  selector: 'app-project-activities',
  templateUrl: './project-activities.component.html',
  styleUrls: ['./project-activities.component.sass']
})
export class ProjectActivitiesComponent implements OnInit {

  @Input() projectId: number;
  allActivities: Activity[];
  filteredActivities: Activity[];
  from: Date;
  to: Date;
  filterUserName: string;
  constructor(private projectService: ProjectService) { }

  ngOnInit() {
    this.projectService.getProjectActivitiesById(this.projectId).subscribe(data =>{
      this.allActivities = Object.assign([],data);
      this.filteredActivities = Object.assign([],data);
    });
  }

  onFilterChange(){
    this.filteredActivities = this.allActivities;
    if(this.from != null){
      this.filteredActivities = this.filteredActivities.filter(act => new Date(act.dateTime) >= new Date(this.from));
    }
    if(this.to != null){
      this.filteredActivities = this.filteredActivities.filter(act => new Date(act.dateTime) <= new Date(this.to));
    }
    if(this.filterUserName != null && this.filterUserName != ""){
      this.filteredActivities = this.filteredActivities.filter(act => {
         if(act.user != null){
          act.user.fullName == this.filterUserName
        }
      });
    }
  }

}
