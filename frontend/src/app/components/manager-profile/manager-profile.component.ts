import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-manager-profile',
  templateUrl: './manager-profile.component.html',
  styleUrls: ['./manager-profile.component.sass']
})
export class ManagerProfileComponent implements OnInit {

  ngOnInit(): void {
    this.Manager = { Name : " Sasha Pushkin",
     Avatar : "http://static-29.sinclairstoryline.com/resources/media/2d9080f1-46ec-47b0-3874-d5190c1b02e7-2d9080f146ec47b03874d5190c1b02e7rendition_1_scottthuman5x7bluegradient.jpg?1519078303490",
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