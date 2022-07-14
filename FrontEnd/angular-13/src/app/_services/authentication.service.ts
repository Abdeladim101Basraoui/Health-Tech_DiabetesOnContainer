import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor() { }

  islogged()
  {
    return localStorage.getItem("email")!=null; 
  }
}
