import { AuthService } from '../auth.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AppStateService } from '../app-state.service';


@Injectable({
  providedIn: 'root'
})
export class LandingGuard implements CanActivate {
  
  constructor(private router: Router, private authService: AuthService, private appState: AppStateService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if ( this.appState.LoginStatus ) {
      this.router.navigate(['/dashboard']);
      return false;
    }
    return true;
  }

}