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
  constructor(public dialog: MatDialog, private router: Router, userService: UserService) {
    debugger
    userService.get().subscribe(
      (d)=> {
        this.fullName = d;
        console.log(d);
      },
      err => {
        console.log('err', err);
      }
    );
  }
  ngOnInit(): void {
    this.manager = { 
     id: 1,
     firstName: "Sasha",
     lastName : "Pushkin",
     avatarUrl : "https://cdn.riastatic.com/photos/ria/dom_news_logo/20/2072/207230/207230m.jpg?v=1422268257", // changed due to CORS policy issues
     birthDate : new Date("12.12.1990"),
     registrationDate : new Date("12.12.1990"),
     country : "Ukraine",
     city : "Kyiv",
     region : "Dniorivskiy",
     address : "Dniprovskaya Street",
     postalCode : "02150",
     phone : "+380-95-654-33-24"}

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

    manager : UserProfile
    projects : Project[]

    
}