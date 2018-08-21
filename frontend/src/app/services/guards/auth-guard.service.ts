import { AuthService } from '../auth.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserService } from '../user.service';
import { UserProfile } from '../../models/user-profile';
import { map, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { AppStateService } from '../app-state.service';

@Injectable()
export class AuthGuard implements CanActivate {
  
  constructor(private router: Router, private authService: AuthService,
    private userService: UserService, private appState: AppStateService) { }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
      if ( this.appState.LoginStatus ) {
        if (!this.userService.getCurrrentUser()) {
            return this.userService.getUser().pipe(
                map((user: UserProfile) => {
                  this.userService.updateCurrrentUser(user);
                  return true;    
                })
            )
        } else {
            return true;
        }
        
      }
      this.router.navigate(['/dashboard']);
      return true;
  }

}

// debugger
//           return this.userService.getUser().pipe(
//               switchMap((user: UserProfile) => {
//                 this.userService.saveUser(user);
//                 return of(true);    
//               })
//           )