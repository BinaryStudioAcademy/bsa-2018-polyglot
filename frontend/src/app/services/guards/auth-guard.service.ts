import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserProfile } from '../../models/user-profile';
import { map, skip } from 'rxjs/operators';
import { AppStateService } from '../app-state.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private appState: AppStateService) { }


    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.appState.LoginStatus) {
            if (!this.appState.currentDatabaseUser) {
                return this.appState.getDatabaseUser().pipe(
                    skip(1),
                    map((user: UserProfile) => {
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

