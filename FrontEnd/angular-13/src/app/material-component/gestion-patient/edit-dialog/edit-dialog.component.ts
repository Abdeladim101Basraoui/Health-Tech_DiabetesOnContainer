import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import {default as _rollupMoment} from 'moment';


@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css'],
 providers: [
    // `MomentDateAdapter` and `MAT_MOMENT_DATE_FORMATS` can be automatically provided by importing
    // `MatMomentDateModule` in your applications root module. We provide it at the component level
    // here, due to limitations of our example generation script.
    {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
    {provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS},
  ],
})
export class EditDialogComponent implements OnInit {

    //props
    patientForm!:FormGroup;

    email = new FormControl('', [Validators.required, /*Validators.email*/]);
    cin = new FormControl("", [Validators.required]);
    nom = new FormControl('', [Validators.required]);
    datenaissance = new FormControl('',[Validators.required]);
    gendervalidate = new FormControl('',[Validators.required]);
    
    constructor(private formbuilder:FormBuilder) { }
    
    ngOnInit(): void {
      this.patientForm= this.formbuilder.group(
        {
          email:this.email,
          cin:this.cin,
          nom:this.nom,
          prenom:this.nom,
          // date: this.datenaissance.value.toISOString().split('T')[0],// this.datenaissance,
          date:  this.datenaissance,
          sexe:this.gendervalidate
        }
      )
    }
    


  //gender
  selectedValue!: string;
  gender: gender[] =
    [
      { value: 'H', viewValue: 'home' },
      { value: 'F', viewValue: 'Femme' },
    ];


  // email validator
  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }
    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  //button methods
  PostPatient() {
console.log(this.patientForm.value);

  }
  Close() {

  }



}
interface gender {
  value: string;
  viewValue: string;
}