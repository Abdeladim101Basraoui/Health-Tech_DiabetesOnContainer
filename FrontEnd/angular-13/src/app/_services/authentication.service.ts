import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { loginUser } from '../_models/LoginUser';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

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
    if (credentials.role == "Doc") {
      link = 'https://localhost:7146/api/admin/Diabeticiens/login';
    }

    if (credentials.role == "Assist") {
      link = 'https://localhost:7146/api/admin/Assistants/login';
    }

    this.http.post(link, { 'email': credentials.email, "password": credentials.password }, { responseType: 'text' })
      .subscribe(response => {
        const token = response;
        console.log(token);
        localStorage.setItem("JWT", token);
        this._isLoggedIn.next(true);
        this.routing.navigate(['']);

      }, err => {
        console.log(err);

        this.routing.navigate(['login']);
      }
      )
  }


//
GetTest() {
  //
  const apiUrl ='https://localhost:7146/api/Patients';
const token  = localStorage.getItem('JWT');
const httpheader = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
})
 this.http.get(apiUrl, { headers: httpheader }).subscribe(response => {
   //
   console.log(response);

  }, err => {
    console.log(err);

    this.routing.navigate(['login']);
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

