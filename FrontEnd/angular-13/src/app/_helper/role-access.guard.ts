import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RoleAccessGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree 
    {
        if (this.Service.RoleCanAccess(route.url[0].path)) {
          return true;
        } else {
          alert("403 :/");
          this.Routing.navigate(['/']);
          return false;
  

        }
      }

        /**
         *
         */
        constructor
          (
            private Service: AuthenticationService,
            private Routing: Router
          ) {

        }

}
