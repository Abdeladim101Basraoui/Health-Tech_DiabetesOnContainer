import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor() { }

    islogged() {
    const role = localStorage.getItem("role");
    const password = localStorage.getItem("password");
    return (role != null && role != '' && password != null && role != '') ? true : false;
  }

  RoleCanAccess(accessmanu: any) {
    const Role = localStorage.getItem('role');

    if (Role?.toLowerCase().trim() == 'doc') {
      return true;
    }
    else {
      if (accessmanu == "register") {   
        return false;
      }
      else if (Role?.toLowerCase().trim() == "assist") { return true; }
      else{

        return false;
      }
    }
  }
}
