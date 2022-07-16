import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, NgForm, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { loginUser, Roles } from "src/app/_models/LoginUser";
import { AuthenticationService } from "src/app/_services/authentication.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  hide = true;
  // selectedRole!:string;

  // form: FormGroup = new FormGroup({
  //   username: new FormControl(""),
  //   password: new FormControl(""),
  // });

  // logincheck(role: any, password: any) {
  //   localStorage.setItem("role", role);
  //   localStorage.setItem('password', password);

  //   console.log(`${role}    ${password}`);

  //   this.Route.navigate(['']);
  // }

  roleControl = new FormControl(null, Validators.required);
  emailFormControl = new FormControl("", Validators.required);
  selectFormControl = new FormControl("", Validators.required);


  login(email: any, _password: any) {
    const credentials :loginUser = {
      role: this.roleControl.value,
      email: email,
      password: _password
    };
    this.authservice.ServerLogin(credentials);
  }

  
getvalues()
{
  this.authservice.GetTest();
}

  /**
   *
   */
  constructor(private authservice:AuthenticationService) {
        
  }
  ngOnInit(): void {}
  roles: Roles[] = [
    {
      value: "Doc",
      viewValue: "Doctore",
    },
    {
      value: "Assist",
      viewValue: "Assistant",
    },
  ];
}

