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


  selectedOption: string = "translator";
  userToReceive: UserProfile;
  error: string;

  @Output() onRoleChoose: EventEmitter<any> = new EventEmitter<any>(true);

  ngOnInit() {
  }

  onSubmit(){
    this.onRoleChoose.emit();
  }

  saveDataInDb(){
    
    let role: Role;
    if(this.selectedOption === "translator"){
      role = Role.Translator;
    }
    if(this.selectedOption === "manager"){
      role = Role.Manager;
    }

    this.userToReceive = {
      fullName: this.data.fullName,
      userRole: role,
      
    }

    return this.userService.create(this.userToReceive);
  }
}