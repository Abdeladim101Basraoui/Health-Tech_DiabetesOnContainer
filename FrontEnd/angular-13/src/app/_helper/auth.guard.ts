import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (this.AuthService.islogged()) {
      return true;
    }
    else {
      this.Route.navigate(['login']);
      return false;
    }

    return true;
  }


  /**
   *
   */
  constructor(
    private AuthService: AuthenticationService,
    private Route: Router) {

  }

}
