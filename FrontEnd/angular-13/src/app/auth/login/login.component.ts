import { NgForOf } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
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

  logincheck(role: any, password: any) {
    localStorage.setItem("role", role);
    localStorage.setItem('password', password);

    console.log(`${role}    ${password}`);
  
    this.Route.navigate(['']);
  }


  constructor(private Route: Router) {

  }

  ngOnInit(): void {
  }

}

