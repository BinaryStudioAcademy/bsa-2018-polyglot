import { Component, OnInit } from '@angular/core';
import { IUserSignUp } from '../../models/user-signup';
import { AuthService } from '../../services/auth.service';
import { MatDialogRef, MatDialog } from '@angular/material';
import { Router } from '@angular/router';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';
import { AppStateService } from '../../services/app-state.service';
import { ChooseRoleDialogComponent } from '../choose-role-dialog/choose-role-dialog.component';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-signup-dialog',
  templateUrl: './signup-dialog.component.html',
  styleUrls: ['./signup-dialog.component.sass']
})
export class SignupDialogComponent implements OnInit {

  public user: IUserSignUp;
  public firebaseError: string;
  public selectedOption : string;
  private IsNotificationSend: boolean;
  private notificationConfig = {
    timeout: 10000,
    showProgressBar: true,
    closeOnClick: false,
    pauseOnHover: false        
  }

  constructor(
    public dialogRef: MatDialogRef<SignupDialogComponent>,
    private authService : AuthService,
    private router: Router,
    private snotify: SnotifyService,
    private appState: AppStateService,
    private dialog: MatDialog,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: '',
      repeatPass: '',
      fullname: ''
    };

    this.selectedOption = "translator";

    this.IsNotificationSend = false;
  }

  onContinueClick(user: IUserSignUp, form) {
    if (form.valid) {
      //open dialog with role choose
      let dialogRef = this.dialog.open(ChooseRoleDialogComponent, {
        data: {
          fullName: user.fullname,
          email: user.email
        }
      });
      const sub = dialogRef.componentInstance.onRoleChoose.subscribe(()=>{  
        //when role choose..
        this.authService.signUpRegular(user.email, user.password).subscribe(
          async (userCred) => {
            this.appState.updateState(userCred.user, await userCred.user.getIdToken(), false);
            //if there is no user with such email send data to database
            dialogRef.componentInstance.saveDataInDb().subscribe(
              (result) => {
                this.authService.sendEmailVerification();
                this.snotify.clear();
                this.snotify.info(`Email confirmation was send to ${userCred.user.email}`, this.notificationConfig);
                this.IsNotificationSend = true;   
                setTimeout(
                  () => dialogRef.close(), 
                  10000);
              },
              (err) => {
                dialogRef.close();
                if (err.code === 'auth/email-already-in-use') {
                  this.snotify.clear();
                  this.snotify.warning(`Email confirmation was already send to ${this.user.email}. Check your email.`, this.notificationConfig);
                }
                this.firebaseError = err.message;
              }
            );
            
          }, 
          (err) => {
            if (err.code === 'auth/email-already-in-use') {
              this.snotify.clear();
              this.snotify.warning(`Email confirmation was already send to ${this.user.email}. Check your email.`, this.notificationConfig);
            }
            dialogRef.componentInstance.error = err.message;
          }
        );
        // not sure that it is logging out 
        this.authService.logout();
      });
  }
  }

  onGoogleClick() {
    this.authService.signInWithGoogle().subscribe(async (userCred) => {
      this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);

        this.userService.getUser().subscribe((data)=>{ //if user is in db
          this.dialogRef.close();
          
        },
        err=>{ //if user is absent in db
        console.log(err);
        let dialogRef = this.dialog.open(ChooseRoleDialogComponent, {
          data: {
            fullName: ''
          }
        });
        const sub = dialogRef.componentInstance.onRoleChoose.subscribe(()=>{  
          dialogRef.componentInstance.saveDataInDb().subscribe(() =>{
            dialogRef.close();
            this.dialogRef.close();
            });
          });
        });
    }, 
      (err) => {
        this.firebaseError = err.message;
      }
    );
  }

  onFacebookClick() {
    this.authService.signInWithFacebook().subscribe(
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);

        if(this.appState.LoginStatus){
          this.dialogRef.close();
        }

        //if exist in db - show error
        this.router.navigate(['/profile/settings']);
      }, 
      (err) => {
        this.firebaseError = err.message;
      }
    );  
  }
}
