import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserProfile } from '../../models/user-profile';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CropperComponent } from '../../dialogs/cropper-dialog/cropper.component';
import { MatDialog } from '@angular/material';
import { UserService } from '../../services/user.service';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import { AppStateService } from '../../services/app-state.service';

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

  constructor(
    private fb: FormBuilder,
    private  dialog: MatDialog,
    private userService: UserService,
    private snotifyService: SnotifyService,
    private appStateService: AppStateService,
    private router: Router
  ) {
    //GET Id here
    //console.log(router.snapshot.params.id);
   }

  ngOnInit() {
    this.manager = this.userService.getCurrentUser();
    if (this.manager.fullName != undefined) {
      var arrayOfStrings = this.manager.fullName.split(' ');
      this.manager.firstName = arrayOfStrings[0];
      this.manager.lastName = arrayOfStrings[1];
    }
    this.createProjectForm();
  }

  editPhoto(){
        const dialogRef = this.dialog.open(CropperComponent, {
            data: {imageUrl: this.manager.avatarUrl}
        });
        dialogRef.afterClosed().subscribe(result => {
            if (dialogRef.componentInstance.cropedImageBlob){
                let formData = new FormData();
                formData.append("image", dialogRef.componentInstance.cropedImageBlob);
                this.userService.updatePhoto(formData).subscribe(
                    (d) => {
                        setTimeout(() => {
                            this.snotifyService.success("Photo updated.", "Success!");
                            }, 100);
                            this.manager = d;
                            this.appStateService.currentDatabaseUser = d;
                        },
                        err => {
                            this.snotifyService.error("Photo failed to update!", "Error!");
                        }
                    );
                }
            }
        );

  }

  createProjectForm(): void {

    this.profileForm = this.fb.group({
      id:[this.manager.id],
      uid:[this.manager.uid],
      userRole:[this.manager.userRole],
      firstName:[this.manager.firstName, [Validators.required]],
      lastName: [this.manager.lastName, [Validators.required]],
      birthDate : [this.manager.birthDate],
      country : [this.manager.country],
      city : [this.manager.city],
      region : [this.manager.region],
      address : [this.manager.address],
      postalCode : [this.manager.postalCode],
      phone : [this.manager.phone],
      avatarUrl : [this.manager.avatarUrl]

    });
  }

  saveChanges(userProfile : UserProfile) {
    userProfile.fullName = `${userProfile.firstName} ${userProfile.lastName}`;
    userProfile.avatarUrl = this.manager.avatarUrl;
    this.userService.update(userProfile.id, userProfile).subscribe(
        (d) => {
            this.appStateService.currentDatabaseUser = d;
            this.manager = d;
            this.snotifyService.success("Saved changes");
            this.router.navigate(['/']);
        }
    );
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

}
