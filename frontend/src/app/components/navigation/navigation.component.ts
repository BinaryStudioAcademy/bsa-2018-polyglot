import { ChangeDetectorRef, Component, OnDestroy, AfterContentInit, DoCheck, AfterContentChecked, AfterViewInit, AfterViewChecked } from '@angular/core';
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
  email: string;
  role: string;

  constructor(
    changeDetectorRef: ChangeDetectorRef,
    media: MediaMatcher,
    public dialog: MatDialog,
    private authService: AuthService,
    private userService: UserService,
    private appState: AppStateService,
    private router: Router
  ) {
    this.mobileQuery = media.matchMedia('(max-width: 960px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener); 
  }

  ngOnInit(): void {
    this.updateCurrentUser();
  }

  onLoginClick() {
    let dialogRef = this.dialog.open(LoginDialogComponent);
    dialogRef.componentInstance.reloadEvent.subscribe(
      () => {
        this.manager = this.userService.getCurrrentUser();
      }
    );
  }

  onSignUpClick() {
    let dialogRef = this.dialog.open(SignupDialogComponent);
    dialogRef.componentInstance.reloadEvent.subscribe(
      () => {
        this.manager = this.userService.getCurrrentUser();
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
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  private updateCurrentUser() {
    if (this.appState.LoginStatus){
      if (!this.userService.getCurrrentUser()) {
        this.userService.getUser().subscribe(
          (user: UserProfile)=> {
            this.userService.updateCurrrentUser(user);   
            this.manager = this.userService.getCurrrentUser();
            // this.email = this.appState.currentFirebaseUser.email;
            this.role = this.manager.userRole == 0 ? 'Translator' : 'Manager';
          },
          err => {
            console.log('err', err);
          }
        );
      } else {
        this.manager = this.userService.getCurrrentUser();
        // this.email = this.appState.currentFirebaseUser.email;
        this.role = this.manager.userRole == 0 ? 'Translator' : 'Manager';
      }
    } else {
      this.manager = { 
        fullName: "",
        avatarUrl: "",
        lastName: "" 
      };
      this.email = '';
      this.role = '';
    }
  }



  
}
