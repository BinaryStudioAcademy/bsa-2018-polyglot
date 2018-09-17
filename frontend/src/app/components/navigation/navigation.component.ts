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
import { Hub } from '../../models/signalrModels/hub';
import { ChatService } from '../../services/chat.service';


@Component({
    providers: [AuthService],
    selector: 'app-navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent implements OnDestroy {

    mobileQuery: MediaQueryList;
    private _mobileQueryListener: () => void;
    private signalRConnection;
    manager: UserProfile;
    notifications: Notification[];
    role: string;
    numberOfUnread: number = 0;

    constructor(
        changeDetectorRef: ChangeDetectorRef,
        media: MediaMatcher,
        public dialog: MatDialog,
        private authService: AuthService,
        private userService: UserService,
        private router: Router,
        private eventService: EventService,
        private appStateService: AppStateService,
        private signalRService: SignalrService,
        private notificationService: NotificationService,
        private chatService: ChatService
    ) {
        this.mobileQuery = media.matchMedia('(max-width: 960px)');
        this._mobileQueryListener = () => changeDetectorRef.detectChanges();
        this.mobileQuery.addListener(this._mobileQueryListener);
        this.eventService.listen().subscribe(
            (event) => {
                switch (event) {
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
        this.appStateService.getFirebaseUser().subscribe(data => {
            if(data){
                this.updateCurrentUser();
                this.chatService.GetNumberOfUnreadMesages().subscribe(n => {
                    this.numberOfUnread = n;
                });
            }
        });
        this.appStateService.getDatabaseUser().subscribe(data => {
            if (data) {
                this.manager = data;

                this.notificationService.getCurrenUserNotifications().subscribe(notifications => {
                    if (notifications) {
                        this.notifications = notifications;
                        this.signalRConnection = this.signalRService.connect(
                            `${SignalrGroups[SignalrGroups.notification]}${
                            this.manager.id
                            }`,
                            Hub.navigationHub
                        );
                        this.subscribeNotificationChanges();
                    }
                });
            }
        });
    }

    subscribeNotificationChanges() {
        this.signalRConnection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.notificationSend],
            (response: any) => {
                if (this.signalRService.validateResponse(response)) {
                    this.notificationService
                        .getCurrenUserNotifications()
                        .subscribe(notifications => {
                            if (notifications) {
                                this.notifications = notifications;
                            }
                        });
                }
            });

            this.signalRConnection.on(
                SignalrSubscribeActions[SignalrSubscribeActions.numberOfMessagesChanges],
                (response: any) => {
                    this.numberOfUnread = response;
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
        this.authService.logout().subscribe(() => { });
    }

    isLoggedIn() {
        return this.appStateService.LoginStatus;
    }

    ngOnDestroy(): void {
        this.signalRService.leaveGroup(
            `${SignalrGroups[SignalrGroups.notification]}${this.manager.id}`,
            Hub.navigationHub
        );
        this.mobileQuery.removeListener(this._mobileQueryListener);
    }

    private updateCurrentUser() {
        if (this.appStateService.LoginStatus) {
            if (!this.userService.getCurrentUser()) {
                this.userService.getUser().subscribe(
                    (user: UserProfile) => {
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
        switch (roleId) {
            case 0:
                roleStr = 'Translator';
                break;
            case 1:
                roleStr = 'Manager';
                break;
        }
        return roleStr;
    }

    getNumberOfUnread(){
        return this.numberOfUnread;
    }
}
