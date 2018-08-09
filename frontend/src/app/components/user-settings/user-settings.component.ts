import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserProfile } from '../../models/user-profile';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CropperComponent } from '../../dialogs/cropper-dialog/cropper.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.sass']
})


export class UserSettingsComponent implements OnInit {

  manager: UserProfile;
  profileForm: FormGroup;
  minDate = new Date(1903, 2, 1);
  maxDate = new Date();


  
  constructor(private router: ActivatedRoute, private fb: FormBuilder, private  dialog: MatDialog,) {
    //GET Id here
    //console.log(router.snapshot.params.id);
   }

  ngOnInit() {
    this.manager = {
     firstName: "Sasha",
     lastName : "Pushkin",
     avatarUrl : "https://cdn.riastatic.com/photos/ria/dom_news_logo/20/2072/207230/207230m.jpg?v=1422268257", // changed due to CORS policy issues
     birthDate : new Date("25/05/2002"),
     registrationDate : new Date("12.12.2017"),
     country : "Ukraine",
     city : "Kyiv",
     region : "Dniorivskiy",
     address : "Dniprovskaya Street",
     postalCode : "02150",
     phone : "+380-95-654-33-24"}
     this.createProjectForm();
  }

  createProjectForm(): void {
    
    this.profileForm = this.fb.group({
      firstName:[this.manager.firstName, [Validators.required]], 
      lastName: [this.manager.lastName, [Validators.required]],
      birthDate : [this.manager.birthDate],
      country : [this.manager.country],
      city : [this.manager.city],
      region : [this.manager.region],
      address : [this.manager.address],
      postalCode : [this.manager.postalCode],
      phone : [this.manager.phone],
      avatarUrl : [this.manager.avatarUrl],

    });
  }

  saveChanges(userProfile : UserProfile) {
    console.log(userProfile);
  }

  
  get firstName() {
    return this.profileForm.get('firstName');
  }

  get lastName() {
    return this.profileForm.get('lastName');
  }

  get birthDate() {
    return this.profileForm.get('birthDate');
  }

  get country() {
    return this.profileForm.get('country');
  }

  get city() {
    return this.profileForm.get('city');
  }

  get address() {
    return this.profileForm.get('address');
  }

  get postalCode() {
    return this.profileForm.get('postalCode');
  }

  get region() {
    return this.profileForm.get('region');
  }

  get phone() {
    return this.profileForm.get('phone');
  }

  editPhoto() {
    this.dialog.open(CropperComponent, {
      data: {imageUrl: this.manager.avatarUrl}
    });
  }

}
