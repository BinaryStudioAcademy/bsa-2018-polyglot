import { Component, OnInit, EventEmitter } from '@angular/core';
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
    timeout: 4000,
    showProgressBar: true,
    closeOnClick: false,
    pauseOnHover: false        
  }

  reloadEvent = new EventEmitter<any>();

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
                  () => {
                    dialogRef.close();
                    this.dialogRef.close();
                  }, 
                  4000);
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
        this.authService.logout().subscribe(() => {});
      });
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
                    async (currentUser) => {
                      this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true, currentUser);
                      this.router.navigate(['/dashboard/projects']);
                      this.reloadEvent.emit(null);
                    }
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
                            async (currentUser) => {
                              this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true, currentUser);
                              this.router.navigate(['/profile/settings']);
                              this.reloadEvent.emit(null);
                            }
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
                  this.router.navigate(['/dashboard/projects']);
                  setTimeout(() => {
                    this.reloadEvent.emit(null);
                  }, 300);
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
                          this.router.navigate(['/profile/settings']);
                          setTimeout(() => {
                            this.reloadEvent.emit(null);
                          }, 300);
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
}
