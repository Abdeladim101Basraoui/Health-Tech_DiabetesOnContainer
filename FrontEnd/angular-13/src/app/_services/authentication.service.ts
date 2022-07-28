import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { loginUser, } from '../_models/LoginUser';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {


  constructor(private http: HttpClient, private routing: Router) {
    //in here we should check the expired date  and refresh the token

    this._isLoggedIn.next(!!localStorage.key(0));
    this._isSuperUser.next(!!localStorage.key(0)?.endsWith('Doc'));

    // console.log(`the logged token ${localStorage.key(0)}`);
    console.log(`is it a Super User ${!!localStorage.key(0)?.endsWith('Doc')}`);


  }

  // //describe the state of the user  -- default state is faulse
  private _isLoggedIn = new BehaviorSubject<boolean>(false);
  private _isSuperUser = new BehaviorSubject<boolean>(false);

  // public _isLoggedIn :boolean = !!localStorage.getItem('JWT Doc') || !!localStorage.getItem('JWT Assist');

  // //any subscriber to this one will be notified of changes
  public isLoggedIn = this._isLoggedIn.asObservable();
  public isSuperUser = this._isSuperUser.asObservable();

  public tokenName: string = '';
  ServerLogin(credentials: loginUser) {

    let link: string = '';
    if (credentials.role == "Doc") {
      link = `${environment.baseAPIUrl}/admin/Diabeticiens/login`;
      this.tokenName = 'JWT Doc'
    }

    if (credentials.role == "Assist") {
      link = `${environment.baseAPIUrl}/admin/Assistants/login`;
      this.tokenName = 'JWT Assist'
    }

    return this.http.post(link, { 'email': credentials.email, "password": credentials.password }, { responseType: 'text' }).pipe(
      tap((response: any) => {

        //save the token to local storage
        localStorage.setItem(this.tokenName, response);

        //keep the state of the user
        this._isLoggedIn.next(true);

      })
    );

  }


  //
  Register(data: any, role: string) {
    let link: string = '';

    if (role == "Doc") {
      link = `${environment.baseAPIUrl}/admin/Diabeticiens/register`;
    }

    if (role == "Assist") {
      link = `${environment.baseAPIUrl}/admin/Assistants/Register`;
    }


    const token = localStorage.getItem('JWT Doc');
    const httpheader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    })


    this.http.post(link, data, { headers: httpheader })
      .subscribe(response => {
        this.routing.navigate(['']);

      }, err => {
        console.log(err);
      });

  }


  RoleCanAccess(accessmanu: any) {
    const accessRole = localStorage.getItem('JWT Doc');

    if (accessRole) {
      return true;
    }

    return false;
  }
}

