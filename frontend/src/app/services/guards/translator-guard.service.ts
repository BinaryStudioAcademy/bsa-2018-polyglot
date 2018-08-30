import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserService } from '../user.service';
import { AppStateService } from '../app-state.service';
import { Role } from '../../models/role';

@Injectable()
export class TranslatorGuardService implements CanActivate {

  constructor(private router: Router,
    private userService: UserService, 
    private appState: AppStateService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if ( this.appState.LoginStatus ) {
      if (!this.userService.getCurrentUser() && this.userService.getCurrentUser().userRole === Role.Translator) {
        this.router.navigate(['/dashboard']);
        return false;
      }
    }

    return true;
  }
}
