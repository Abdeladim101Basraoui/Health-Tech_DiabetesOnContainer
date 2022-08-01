import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css']
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
          date:this.datenaissance,
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