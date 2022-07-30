import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { loginUser } from "../_models/LoginUser_model";
import { Route } from "@angular/compiler/src/core";
import { Router } from "@angular/router";
import { BehaviorSubject } from "rxjs";
import { environment } from "src/environments/environment";
import { tap } from "rxjs/operators";
import { userRole } from "../_models/userRole_model";

@Injectable({
  providedIn: "root",
})
export class AuthenticationService {
  //call Url
  private readonly patientUrl = "FichePatients";

  //the payload infos
  user!: userRole;

  constructor(private http: HttpClient, private routing: Router) {
    // console.log(this.isLoggedIn);

    if (!!this.Token) {
      //TODO in here we should check the expired date and refresh the token
      
      
      //the state of the token
      this.user = this.getUser(this.Token!);
      
      this._isLoggedIn.next(!!this.Token);

      this._isSuperUser.next(!!this.user.userRole.includes("Doc"));

      // console.log(`the token is ${this.Token}`);
      // console.log(`the date is ${new Date(this.user.exp * 1000)}`);
      // console.log(this.user);
    }
  }

  // -->describe the state of the user  -- default state is faulse
  private _isLoggedIn = new BehaviorSubject<boolean>(false);
  private _isSuperUser = new BehaviorSubject<boolean>(false);

  // public _isLoggedIn :boolean = !!localStorage.getItem('JWT Doc') || !!localStorage.getItem('JWT Assist');

  // -->any subscriber to this one will be notified of changes
  public isLoggedIn = this._isLoggedIn.asObservable();
  public isSuperUser = this._isSuperUser.asObservable();

  get Token() {
    return localStorage.getItem(localStorage.key(0)?.toString()!);
  }

  public tokenName: string = "";
  ServerLogin(credentials: loginUser) {
    let link: string = "";

    //TODO the solution of the conditions is in the backend
    if (credentials.role == "Doc") {
      link = `${environment.baseAPIUrl}/admin/Diabeticiens/login`;

    }

    if (credentials.role == "Assist") {
      link = `${environment.baseAPIUrl}/admin/Assistants/login`;

    }
//return the observer
    return this.http
      .post(
        link,
        { email: credentials.email, password: credentials.password },
        { responseType: "text" }
      )
      .pipe(
        tap((response: any) => {
          //save the token to local storage
          this.user = this.getUser(response);

          this.tokenName = `JWT ${this.user.userRole}`;
          localStorage.setItem(this.tokenName, response);
          
          //keep the state of the user
          this._isLoggedIn.next(true);
          this._isSuperUser.next(this.user.userRole.includes('Doc'));
        })
      );
      ;
  }

  //
  Register(data: any, role: string) {
    let link: string = "";

    if (role == "Doc") {
      link = `${environment.baseAPIUrl}/admin/Diabeticiens/register`;
    }

    if (role == "Assist") {
      link = `${environment.baseAPIUrl}/admin/Assistants/Register`;
    }

    const token = localStorage.key(0)?.toString();
    // const httpheader = new HttpHeaders({
    //   'Content-Type': 'application/json',
    //   'Authorization': `Bearer ${token}`
    // })

    this.http.post(link, data /*,{ headers: httpheader }*/).subscribe(
      (response) => {
        this.routing.navigate([""]);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  RoleCanAccess(accessmanu: any) {
    const accessRole = localStorage.getItem("JWT Doc");

    if (accessRole) {
      return true;
    }

    return false;
  }

  getFichePatient() {}

  //get the data from the token {role <==> exp date}
  private getUser(token: string): userRole {
    // return JSON.parse(atob(token.split('.')[1])) as userRole;
    // this._isSuperUser.next(!!this.user.role.includes('Doc'));
    return JSON.parse(atob(token.split(".")[1]));
  }
}
