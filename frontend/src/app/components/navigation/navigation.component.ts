import { ChangeDetectorRef, Component, OnDestroy, AfterContentInit, DoCheck, AfterContentChecked, AfterViewInit, AfterViewChecked, Output } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LoginDialogComponent } from '../../dialogs/login-dialog/login-dialog.component';
import { SignupDialogComponent } from '../../dialogs/signup-dialog/signup-dialog.component';
import { StringDialogComponent } from '../../dialogs/string-dialog/string-dialog.component';
import { AuthService } from '../../services/auth.service';
import { MediaMatcher } from '@angular/cdk/layout';
import { UserService } from '../../services/user.service';
import { UserProfile } from '../../models';
import { map } from 'rxjs/operators';
import { AppStateService } from '../../services/app-state.service';
import { Router } from '@angular/router';
import { EventService } from '../../services/event.service';
import { SignalrService } from '../../services/signalr.service';
import { SignalrGroups } from '../../models/signalrModels/signalr-groups';
import { SignalrSubscribeActions } from '../../models/signalrModels/signalr-subscribe-actions';
import { NotificationService } from '../../services/notification.service';
import { Notification } from '../../models/notification';


@Component({
  providers: [AuthService],
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent implements OnDestroy {

  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  manager : UserProfile;
  notifications: Notification[];
  role: string;

  constructor(
    changeDetectorRef: ChangeDetectorRef,
    media: MediaMatcher,
    public dialog: MatDialog,
    private authService: AuthService,
    private userService: UserService,
    private appState: AppStateService,
    private router: Router,
    private eventService: EventService,
    private appStateService: AppStateService,
    private signalRService: SignalrService,
    private notificationService: NotificationService
  ) {
    this.mobileQuery = media.matchMedia('(max-width: 960px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
    this.eventService.listen().subscribe(
      (event) => {
        switch(event) {
          case 'signUp':
            this.onSignUpClick();
            break;
          case 'login':
            this.onLoginClick();
            break;
        }
      }
    );
  }

  ngOnInit(): void {
    this.updateCurrentUser();
    this.appStateService.getDatabaseUser().subscribe(data => {
      if(data){
        this.manager = data;

        this.notificationService.getCurrenUserNotifications().subscribe(notifications => {
          if (notifications) {
            this.notifications = notifications;
            this.signalRService.createConnection(
              `${SignalrGroups[SignalrGroups.notification]}${
                this.manager.id
                }`,
                "navigationHub"
            );
            this.subscribeNotificationChanges();
          }
        });
      }
    });
  }

  subscribeNotificationChanges(){
    this.signalRService.connection.on(            
      SignalrSubscribeActions[SignalrSubscribeActions.notificationSend],
      (response: any) => {
          if (this.signalRService.validateResponse(response)) {
              this.notificationService
                  .getCurrenUserNotifications()
                  .subscribe(notifications => {
                      console.log(notifications);
                      if (notifications) {
                        this.notifications = notifications;
                      }
                  });
          }
      });
  }

  onLoginClick() {
    let dialogRef = this.dialog.open(LoginDialogComponent);
    dialogRef.componentInstance.reloadEvent.subscribe(
      () => {
        this.manager = this.userService.getCurrentUser();
        this.role = this.roleToString(this.manager.userRole);
      }
    );
  }

  onSignUpClick() {
    let dialogRef = this.dialog.open(SignupDialogComponent);
    dialogRef.componentInstance.reloadEvent.subscribe(
      () => {
        this.manager = this.userService.getCurrentUser();
        this.role = this.roleToString(this.manager.userRole);
      }
    );
  }

  onNewStrClick() {
    this.dialog.open(StringDialogComponent);
  }

  onLogoutClick() {
    this.authService.logout();
    this.appState.updateState(null, '', false, null);
    this.userService.removeCurrentUser();
    this.router.navigate(['/']);
  }

  isLoggedIn() {
    return this.appState.LoginStatus;
  }

  ngOnDestroy(): void {
    this.signalRService.closeConnection(
      `${SignalrGroups[SignalrGroups.notification]}${this.manager.id}`
  );
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  private updateCurrentUser() {
    if (this.appState.LoginStatus){
      if (!this.userService.getCurrentUser()) {
        this.userService.getUser().subscribe(
          (user: UserProfile)=> {
            this.userService.updateCurrentUser(user);
            this.manager = this.userService.getCurrentUser();
            this.role = this.roleToString(this.manager.userRole);
          },
          err => {
            console.log('err', err);
          }
        );
        // this.email = this.appState.currentFirebaseUser.email;
      }
      else {
        this.manager = this.userService.getCurrentUser();
      }
    } else {
      this.manager = {
        fullName: "",
        avatarUrl: "",
        lastName: ""
      };
      // this.email = '';
      this.role = '';
    }
  }

  roleToString(roleId: number) {
    let roleStr: string;
    switch(roleId) {
      case 0:
        roleStr = 'Translator';
        break;
      case 1:
        roleStr = 'Manager';
        break;
    }
    return roleStr;
  }
}
