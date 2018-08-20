import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material';
import { IUserLogin } from '../../models';
import { AuthService } from '../../services/auth.service';
import { SnotifyService } from 'ng-snotify';
import { ForgotPasswordDialogComponent } from '../forgot-password-dialog/forgot-password-dialog.component';
import { AppStateService } from '../../services/app-state.service';
import { UserService } from '../../services/user.service';
import { ChooseRoleDialogComponent } from '../choose-role-dialog/choose-role-dialog.component';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.sass']
})
export class LoginDialogComponent implements OnInit {

  public user: IUserLogin;
  public firebaseError: string;
  hide = true;
  notificationConfig = {
    timeout: 15000,
    showProgressBar: true,
    closeOnClick: false,
    pauseOnHover: false
  }

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    private authService : AuthService,
    private snotify: SnotifyService,
    public dialog: MatDialog,
    private appState: AppStateService,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: ''
    };
  }

  onLoginFormSubmit(user: IUserLogin, form) {
    if (form.valid) {
      this.authService.signInRegular(user.email, user.password).subscribe(
        async (userCred) => {
          if (userCred.user.emailVerified) {
            this.appState.updateState(userCred.user, await userCred.user.getIdToken(), false);
            this.userService.getUser().subscribe(
              async (currentUser) => {
                this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true, currentUser);
                this.dialogRef.close();
              }
            );    
          } else {
            this.snotify.clear();
            this.snotify.warning(`Email confirmation was already send to ${userCred.user.email}. Check your email.`, {
              timeout: 15000,
              showProgressBar: true,
              closeOnClick: false,
              pauseOnHover: false,
              buttons: [
                {text: 'Resend', action: () => {
                  userCred.user.sendEmailVerification();
                  this.authService.logout();
                  this.snotify.clear();
                  this.snotify.info(`Email confirmation was send to ${userCred.user.email}`, this.notificationConfig);
                }}
              ]
            }
            );
            this.firebaseError = 'You need to confirm your email address in order to use our service';
          }
        }, 
        (err) => {
          this.firebaseError = this.handleFirebaseErrors(err);
        }
      );
    }
  }

  onGoogleClick() {
    let dialogRef: MatDialogRef<ChooseRoleDialogComponent>;
    this.authService.signInWithGoogle().subscribe( 
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), false);
        
        this.dialogRef.afterClosed().subscribe(
          () => {
            this.userService.isUserInDb().subscribe(
              (is) => {
                if (is) {
                  this.userService.getUser().subscribe(
                    async (currentUser) => this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true, currentUser)
                  );                 
                } else {
                  dialogRef = this.dialog.open(ChooseRoleDialogComponent, {
                    data: {
                      fullName: '',
                      email: ''
                    }
                  });
                  dialogRef.componentInstance.onRoleChoose.subscribe(
                    () => {
                      dialogRef.componentInstance.saveDataInDb().subscribe(
                        (result) => {
                          dialogRef.close();
                          this.userService.getUser().subscribe(
                            async (currentUser) => this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true, currentUser)
                          );     
                        },
                        (err) => {
                          dialogRef.componentInstance.error = err.message;
                        }
                      );
                    }
                  );
                }
              }
            );
          }
        );
        
        this.dialogRef.close(); 
      }, 
      (err) => {
        this.firebaseError = err.message;
      }); 
  }

  onFacebookClick() {
    let dialogRef: MatDialogRef<ChooseRoleDialogComponent>;
    this.authService.signInWithFacebook().subscribe(
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), false);
        
        this.dialogRef.afterClosed().subscribe(
          () => {
            this.userService.isUserInDb().subscribe(
              async (is) => {
                if (is) {
                  this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);
                } else {
                  dialogRef = this.dialog.open(ChooseRoleDialogComponent, {
                    data: {
                      fullName: '',
                      email: ''
                    }
                  });
                  dialogRef.componentInstance.onRoleChoose.subscribe(
                    () => {
                      dialogRef.componentInstance.saveDataInDb().subscribe(
                        async (result) => {
                          dialogRef.close();
                          this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);
                        },
                        (err) => {
                          dialogRef.componentInstance.error = err.message;
                        }
                      );
                    }
                  );
                }
              }
            );
          }
        );

        this.dialogRef.close();
      }, 
      (err) => {
        this.firebaseError = err.message;
      });
  }

  onForgotPasswordClick() {
    this.dialogRef.close();
    this.dialog.open(ForgotPasswordDialogComponent);
  }

  private handleFirebaseErrors(error) {
    var result;
    switch(error.code) {
      case 'auth/wrong-password': 
      case 'auth/user-not-found':
        result = 'Wrong email or password';
        break;
      default: 
        result = error.message;
        break;
    }
    return result;
  }
}
