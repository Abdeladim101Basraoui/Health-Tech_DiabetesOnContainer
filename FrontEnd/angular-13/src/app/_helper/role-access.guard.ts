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

    return this.AccessVerify();
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {

    return this.AccessVerify();
  }

  AccessVerify() {
    return this.authservice.isSuperUser.pipe(
      tap((isSuper) => {
        if (!isSuper) {
          alert(`403 thank you`);
          this.route.navigate(['']);
        }
      })
    );

  //---------------
  // return this.authservice.isSuperUser;
  }

  /**
   *
   */
  constructor(private route: Router, private authservice: AuthenticationService) {

  }
}
