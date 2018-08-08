import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CropperComponent } from '../../dialogs/cropper-dialog/cropper.component';

@Component({
  selector: 'app-manager-profile',
  templateUrl: './manager-profile.component.html',
  styleUrls: ['./manager-profile.component.sass']
})
export class ManagerProfileComponent implements OnInit {
  
  constructor(
    public dialog: MatDialog
  ) {
  }
  ngOnInit(): void {
    this.Manager = { Name : " Sasha Pushkin",
     Avatar : "https://i.imgur.com/6blJ0sz.jpg", // changed due to CORS policy issues
     Birth : "25.05.2122",
     RegistrationDate : "12.12.1222",
     Country : "Ukraine",
     City : "Kyiv",
     Region : "Dniorivskiy",
     Address : "Dniprovskaya Street",
     PostalCode : "02150",
     Phone : "+380-95-654-33-24"}

     this.Projects = [
        { Name : "Translation", Technology : "Machine"},
        { Name : "Translation", Technology : "Human "},
        { Name : "Translation", Technology : "Machine"},
        { Name : "Translation", Technology : "Human "},
        { Name : "Translation", Technology : "Machine"},
        { Name : "Translation", Technology : "Human "}
     ]
     
  }

  editPhoto(){
    this.dialog.open(CropperComponent, {
      data: {imageUrl: this.Manager.Avatar}
    });
  }

Manager : Manager
Projects : Project[]
}

export interface Manager{
Name : string
Avatar : string
Birth : string
RegistrationDate : string
Country :string
City :string
Region :string
Address : string
PostalCode :string
Phone : string}

export interface Project{
  Name : string,
  Technology : string,
}