import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { AuthService } from '../../services/auth.service';
import { SnotifyService } from 'ng-snotify';
import { UserService } from '../../services/user.service';
import { UserProfile } from '../../models';
import { Role } from '../../models/role';

@Component({
  selector: 'app-choose-role-dialog',
  templateUrl: './choose-role-dialog.component.html',
  styleUrls: ['./choose-role-dialog.component.sass']
})
export class ChooseRoleDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private snotify: SnotifyService,
              public dialogRef: MatDialogRef<ChooseRoleDialogComponent>,
              private userService: UserService) {  
              }


  selectedOption: string = 'Translator';
  userToReceive: UserProfile;
  error: string;

  @Output() onRoleChoose: EventEmitter<any> = new EventEmitter<any>(true);

  ngOnInit() {
  }

  onSubmit(){
    this.onRoleChoose.emit(null);
  }

  saveDataInDb(){
    let role: Role;
    if(this.selectedOption === 'Translator'){
      role = Role.Translator;
    }
    if(this.selectedOption === 'Manager'){
      role = Role.Manager;
    }

    this.userToReceive = {
      fullName: this.data.fullName,
      userRole: role
    }

    console.log(this.userToReceive);

    return this.userService.create(this.userToReceive);
  }
}