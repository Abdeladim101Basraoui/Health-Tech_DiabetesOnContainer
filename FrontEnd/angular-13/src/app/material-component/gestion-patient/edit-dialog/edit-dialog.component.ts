import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RequestsService } from 'src/app/_services/requests.service';
import { GestionPatientComponent } from '../gestion-patient.component';
// import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter } from '@angular/material-moment-adapter';
// import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
// import * as _moment from 'moment';
// // tslint:disable-next-line:no-duplicate-imports
// import { default as _rollupMoment } from 'moment';
import { patient_Cud, patient_put } from 'src/app/_models/requests_models';
import { DialogComponent } from '../../dialog/dialog.component';


// const moment = _rollupMoment || _moment;

@Component({
    selector: 'app-edit-dialog',
    templateUrl: './edit-dialog.component.html',
    styleUrls: ['./edit-dialog.component.css'],
    // providers: [
    //     // `MomentDateAdapter` and `MAT_MOMENT_DATE_FORMATS` can be automatically provided by importing
    //     // `MatMomentDateModule` in your applications root module. We provide it at the component level
    //     // here, due to limitations of our example generation script.
    //     { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    //     { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
    // ],
})

export class EditDialogComponent implements OnInit {

    //props
    patientForm!: FormGroup;

    email = new FormControl('', [Validators.required, /*Validators.email*/]);
    cin = new FormControl("", [Validators.required]);
    nom = new FormControl('', [Validators.required]);
    prenom = new FormControl('', [Validators.required]);
    datenaissance = new FormControl('', Validators.required);
    gendervalidate = new FormControl('', [Validators.required]);

    selectedDate!: Date;

    confirmBtnstate: string = 'save';

    constructor(private formbuilder: FormBuilder, private requestService: RequestsService, private dialogRef: MatDialogRef<DialogComponent>,
        @Inject(MAT_DIALOG_DATA) public editData: any
    ) { }


    ngOnInit(): void {

        this.patientForm = this.formbuilder.group(
            {
                email: this.email,
                cin: this.cin,
                nom: this.nom,
                prenom: this.prenom,
                date: this.datenaissance,
                sexe: this.gendervalidate
            }
        )
        if (this.editData) {
            this.confirmBtnstate = 'update';
            this.patientForm.controls['email'].setValue(this.editData.email);
            this.patientForm.controls['cin'].setValue(this.editData.cin);
            this.patientForm.controls['nom'].setValue(this.editData.nom);
            this.patientForm.controls['prenom'].setValue(this.editData.prenom);
            this.patientForm.controls['date'].setValue(this.editData.dateNaissance);
            console.log(this.editData.dateNaissance);
            this.selectedValue = this.editData.sexe;
        }

        // this.patientForm.controls['date'].setValue(datevalue);
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
    PostPatient(_date: Date) {
        if (this.patientForm.valid || !this.email.valid) {

            if (this.editData) {
                let data: patient_put =
                {
                    dateNaissance: _date,
                    email: this.email.value,
                    nom: this.nom.value,
                    prenom: this.prenom.value,
                    sexe: this.gendervalidate.value
                }
                this.requestService.putPatient(data,this.editData.cin).subscribe(
                    res => {
                        alert('updated successfuly');
                        this.dialogRef.close('update');
                        this.patientForm.reset();
                    }, err => {
                        console.log(err);
                    }
                )
            }
            else {
                let data: patient_Cud = {
                    nom: this.nom.value,
                    prenom: this.prenom.value,
                    email: this.email.value,
                    sexe: this.gendervalidate.value,
                    dateNaissance: _date,
                    cin: this.cin.value
                }
                console.log(data);

                this.requestService.postPatient(data).subscribe
                    (
                        (response) => {
                            alert(`the product is addedd successfuly`)
                            this.dialogRef.close('save');
                            this.patientForm.reset();
                        },
                        (err) => {
                            console.log(err);
                        }
                    );
            }
        }
        else {
            alert('fill all the field');
        }
    }





}
interface gender {
    value: string;
    viewValue: string;
}