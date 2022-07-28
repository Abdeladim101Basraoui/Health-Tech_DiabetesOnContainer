import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable()
export class AuthRequestsInterceptor implements HttpInterceptor {

  constructor(private authservice:AuthenticationService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
  request = request.clone(
{
  headers:request.headers.set('Authorization',`Bearer ${this.authservice.Token}`)
      // const httpheader = new HttpHeaders({
    //   'Content-Type': 'application/json',
    //   'Authorization': `Bearer ${token}`
    // })
}
  );
    return next.handle(request);
  }
}
