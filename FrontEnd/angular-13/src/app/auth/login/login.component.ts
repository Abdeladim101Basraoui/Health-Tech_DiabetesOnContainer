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

