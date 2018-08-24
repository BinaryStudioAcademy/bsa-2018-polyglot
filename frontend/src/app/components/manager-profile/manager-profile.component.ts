import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CropperComponent } from '../../dialogs/cropper-dialog/cropper.component';
import { Project } from '../../models/project';
import { UserProfile } from '../../models/user-profile';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { ProjectService } from '../../services/project.service';

@Component({
  selector: 'app-manager-profile',
  templateUrl: './manager-profile.component.html',
  styleUrls: ['./manager-profile.component.sass']
})
export class ManagerProfileComponent implements OnInit {
  
  fullName : string;
  manager : UserProfile
  projects : Project[]

  constructor(
    public dialog: MatDialog, 
    private router: Router, 
    private userService: UserService,
    private projectService: ProjectService) {
  }
  

  ngOnInit(): void {
    this.manager = this.userService.getCurrentUser();

    this.projectService.getAll().subscribe(pr => {
        this.projects = pr;
    });
     
  }

  editPhoto(){
    this.dialog.open(CropperComponent, {
      data: {imageUrl: this.manager.avatarUrl}
    });
  }



    
}