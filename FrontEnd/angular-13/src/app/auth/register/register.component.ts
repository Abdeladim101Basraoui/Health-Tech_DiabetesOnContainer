
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterAssist, RegisterDoc, Roles } from 'src/app/_models/LoginUser';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  hide = true;
  selectedSex: string = '';

  roleControl = new FormControl(null, Validators.required);

  genderControl = new FormControl(null, Validators.required);

  cinFormControl = new FormControl("", Validators.required);

  nomFormControl = new FormControl("", Validators.required);

  prenomFormControl = new FormControl("", Validators.required);

  emailFormControl = new FormControl("", Validators.required);
  selectFormControl = new FormControl("", Validators.required);

  passFormControl = new FormControl("", Validators.required);

  RefFormControl =new FormControl('',Validators.required);
  constructor(private authservice: AuthenticationService) {


  }



  ngOnInit(): void {
  }


  register(cin: string, nom: string, prenom: string, email: string, password: string): void {
    const role =this.roleControl.value;
    console.log(role);
    
    if( role== 'Doc')
   {
    const credentials:RegisterDoc = {
      password: password,
      email: email,
      cin: cin,
      nom: nom,
      prenom: prenom,
      sexe: this.genderControl.value,
      RefMed:this.RefFormControl.value
    }
    
    this.authservice.Register(credentials,this.roleControl.value);
   }
   else
   {
    const credentials:RegisterAssist= {
      password: password,
      email: email,
      cin: cin,
      nom: nom,
      prenom: prenom,
      sexe: this.genderControl.value
    }

    
    this.authservice.Register(credentials,this.roleControl.value);
   }


  }





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

  gender: Roles[] = [{
    value: 'H',
    viewValue: 'home'
  }, {
    value: 'F',
    viewValue: 'Femme'
  }]
}
