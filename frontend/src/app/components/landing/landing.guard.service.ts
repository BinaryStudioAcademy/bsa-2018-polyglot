import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { map, take, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AuthService } from '../../services/auth.service';



@Injectable()
export class LandingGuard implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthService,
  ) {}


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : Observable<boolean> | boolean{
    return confirm('Ви не авторизовані, все одно хочете перейти за посиланням?');
}

  // canActivate(): Observable<boolean> {
  //   return this.authService.isLoggedIn().pipe(
  //     tap(isLoggedIn => isLoggedIn && this.router.navigate(['/Dashoard'])),
  //     map(isLoggedIn => !isLoggedIn),
  //     take(1),
  //   );
  // }
}
