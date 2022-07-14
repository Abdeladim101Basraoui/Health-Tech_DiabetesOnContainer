import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

logincheck(email:any,password:any)
{
  localStorage.setItem("email",email);
  localStorage.setItem('password',password);

console.log(`${email}    ${password}`);
this.Route.navigate(['']);
}
  //variables

/**
 *
 */
constructor(private Route:Router) {

}

  ngOnInit(): void {
  }

}
