import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { loginUser, } from '../_models/LoginUser';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { getLocaleMonthNames } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {


  constructor(private http: HttpClient, private routing: Router) {
    const token = localStorage.getItem('JWT');
    //in here we should check the expired date  and refresh the token

    this._isLoggedIn.next(!!token);
  }

  //describe the state of the user
  private _isLoggedIn = new BehaviorSubject<boolean>(false);

  //any subscriber to this one will be notified of changes
  public isLoggedIn = this._isLoggedIn.asObservable();

  ServerLogin(credentials: loginUser) {

    let link: string = '';
    let tokenName: string = '';
    if (credentials.role == "Doc") {
      link = 'https://localhost:7146/api/admin/Diabeticiens/login';
      tokenName = 'JWT Doc'
    }

    if (credentials.role == "Assist") {
      link = 'https://localhost:7146/api/admin/Assistants/login';
      tokenName = 'JWT Assist'
    }

    this.http.post(link, { 'email': credentials.email, "password": credentials.password }, { responseType: 'text' })
      .subscribe(response => {

        console.log(response);
        localStorage.setItem(tokenName, response);
        this._isLoggedIn.next(true);
        this.routing.navigate(['']);

      }, err => {
        console.log(err);

        this.routing.navigate(['login']);
      }
      )
  }

  
  //
  Register(data: any, role: string) {
    let link: string = '';

    if (role == "Doc") {
      link = 'https://localhost:7146/api/admin/Diabeticiens/register';
    }

    if (role == "Assist") {
      link = 'https://localhost:7146/api/admin/Assistants/Register';
    }


    const token = localStorage.getItem('JWT Doc');
    const httpheader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    })


    this.http.post(link, data, { headers: httpheader })
      .subscribe(response => {
        this.routing.navigate(['/']);

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

  //
  //
  GetTest() {
    //
    const apiUrl = 'https://localhost:7146/api/Patients';
    const token = localStorage.getItem('JWT');
    const httpheader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    })
    this.http.get(apiUrl, { headers: httpheader }).subscribe(response => {
      //
      console.log(response);

    }, err => {
      console.log(err);
    }
    )
  }

}






  //   islogged() {
  //   const role = localStorage.getItem("role");
  //   const password = localStorage.getItem("password");
  //   return (role != null && role != '' && password != null && role != '') ? true : false;
  // }


  // RoleCanAccess(accessmanu: any) {
  //   const Role = localStorage.getItem('role');

  //   if (Role?.toLowerCase().trim() == 'doc') {
  //     return true;
  //   }
  //   else {
  //     if (accessmanu == "register") {   
  //       return false;
  //     }
  //     else if (Role?.toLowerCase().trim() == "assist") { return true; }
  //     else{

  //       return false;
  //     }
  //   }
  // }
