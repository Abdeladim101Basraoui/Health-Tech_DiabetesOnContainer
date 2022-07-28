import { TagPlaceholder } from "@angular/compiler/src/i18n/i18n_ast";
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
export class AuthGuard implements CanActivate, CanActivateChild {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.LoggingLogic();

  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.LoggingLogic();
  }

  LoggingLogic() {
    return this.authservice.isLoggedIn.pipe(
      tap((islogged) => {
        if (!islogged) {
          this.route.navigate(['/login']);
        }
      })
    );
  }

  /**
   *
   */
  constructor(
    private authservice: AuthenticationService,
    private route: Router
  ) { }
}
