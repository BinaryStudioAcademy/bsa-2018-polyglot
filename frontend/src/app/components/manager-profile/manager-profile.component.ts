import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CropperComponent } from '../../dialogs/cropper-dialog/cropper.component';
import { Project } from '../../models/project';
import { UserProfile } from '../../models/user-profile';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';

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
    private userService: UserService) {
  }
  

  ngOnInit(): void {
    this.manager = this.userService.getCurrrentUser();
    // this.manager = { 
    //  id: 1,
    //  firstName: "",
    //  lastName : "",
    //  avatarUrl : "", // changed due to CORS policy issues
    //  birthDate : new Date("12.12.1990"),
    //  registrationDate : new Date("12.12.1990"),
    //  country : "Ukraine",
    //  city : "Kyiv",
    //  region : "Dniorivskiy",
    //  address : "Dniprovskaya Street",
    //  postalCode : "02150",
    //  phone : "+380-95-654-33-24"}

     this.projects = [
        { name : "Translation", technology : "Machine"},
        { name : "Translation", technology : "Human "},
        { name : "Translation", technology : "Machine"},
        { name : "Translation", technology : "Human "},
        { name : "Translation", technology : "Machine"},
        { name : "Translation", technology : "Human "}
     ]
     
  }

  editPhoto(){
    this.dialog.open(CropperComponent, {
      data: {imageUrl: this.manager.avatarUrl}
    });
  }



    
}