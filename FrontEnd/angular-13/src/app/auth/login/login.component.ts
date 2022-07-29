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

    let tokenName: string = '';
    if (credentials.role == "Doc") {
      tokenName = 'JWT Doc'
    }
    if (credentials.role == "Assist") {
      tokenName = 'JWT Assist'
    }

    this.authservice.ServerLogin(credentials).subscribe(response => {
        this.routing.navigate(['']);
      }, err => {
        console.log(err);
        this.routing.navigate(['login']);
      }
      );
  }

  

  /**
   *
   */
  constructor(private authservice:AuthenticationService,private routing:Router) {
        
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

