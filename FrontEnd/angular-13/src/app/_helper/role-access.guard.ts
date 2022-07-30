import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from "@angular/router";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { AuthenticationService } from "../_services/authentication.service";

@Injectable({
  providedIn: "root",
})
export class RoleAccessGuard implements CanActivate, CanActivateChild {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {

    return this.AccessVerify(route.data.role);
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {

    return this.AccessVerify(childRoute.data.role);
  }

  AccessVerify(tokenrole:string) {
    return this.authservice.isSuperUser.pipe(
      tap((isSuper) => {
        if (!isSuper 
          && !this.authservice.user.userRole.includes(tokenrole)
          ) {
          alert(`403 thank you`);
          this.route.navigate(['']);
        }
      })
    );

  //---------------
  // return this.authservice.isSuperUser;
  //this.authservice.user.userRole.includes(tokenrole);
  }

  /**
   *
   */
  constructor(private route: Router, private authservice: AuthenticationService) {

  }
}
