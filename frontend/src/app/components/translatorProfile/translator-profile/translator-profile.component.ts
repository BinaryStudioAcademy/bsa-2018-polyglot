import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CropperComponent } from '../../../dialogs/cropper-dialog/cropper.component';
import { UserService } from '../../../services/user.service';
import { UserProfile } from '../../../models/user-profile';

@Component({
  selector: 'app-translator-profile',
  templateUrl: './translator-profile.component.html',
  styleUrls: ['./translator-profile.component.sass']
})
export class TranslatorProfileComponent implements OnInit{

  constructor(
    private userService: UserService,
    public dialog: MatDialog
  ) {
  }

  public userProfile : any;

  ngOnInit(): void {
    this.userService.getOne(4).subscribe(up => {
      this.userProfile = up;
      console.log(this.userProfile);
    });
    this.Translator = { Name : " Sasha Pushkin",
     Avatar : "https://i.imgur.com/LzRiWVA.jpg",
     Birth : "25.05.2122",
     RegistrationDate : "12.12.1222",
     Country : "Ukraine",
     City : "Kyiv",
     Region : "Dniorivskiy",
     Address : "Dniprovskaya Street",
     PostalCode : "02150",
     Phone : "+380-95-654-33-24"}

     this.Comments = [
        { CreatedBy : "Petrov Ivan",Body : "Comment body with text",CreatedOn : "12.12.2018", Rating : 4.9 ,
         Avatar : "http://static-29.sinclairstoryline.com/resources/media/2d9080f1-46ec-47b0-3874-d5190c1b02e7-2d9080f146ec47b03874d5190c1b02e7rendition_1_scottthuman5x7bluegradient.jpg?1519078303490"},
        { CreatedBy : "Savinov Ivan",Body : "Comment body with text",CreatedOn : "08.11.2018", Rating : 7.9 , 
         Avatar : "http://static-29.sinclairstoryline.com/resources/media/2d9080f1-46ec-47b0-3874-d5190c1b02e7-2d9080f146ec47b03874d5190c1b02e7rendition_1_scottthuman5x7bluegradient.jpg?1519078303490"}
     ]

     this.Languages = [
       {Name : "French",Proficiency : 47},
       {Name : "Spanish",Proficiency : 77},
       {Name : "English",Proficiency : 97},
       {Name : "OtherLang",Proficiency : 50},
       {Name : "OtherLang",Proficiency : 50},
       {Name : "OtherLang",Proficiency : 50},
       {Name : "OtherLang",Proficiency : 50}
     ]
     
  }

  editPhoto(){
    this.dialog.open(CropperComponent, {
      data: {imageUrl: this.Translator.Avatar}
    });
  }

Translator : Translator
Comments : Comment[]
Languages : Language []
}

export interface Translator{
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
export interface Comment{
  CreatedBy : string,
  Avatar : string ,
  Body : string,
  CreatedOn : string,
  Rating : number
}
export interface Language{
  Name : string,
  Proficiency : number
}
