import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { fichepatient_post } from 'src/app/_models/requests_models';
import { RequestsService } from 'src/app/_services/requests.service';

@Component({
  selector: 'app-edit-quickconsult-dialog',
  templateUrl: './edit-quickconsult-dialog.component.html',
  styleUrls: ['./edit-quickconsult-dialog.component.css']
})
export class EditQuickconsultDialogComponent implements OnInit {

  quickConsultform!: FormGroup;

  refMed = new FormControl('', Validators.required);
  nompres = new FormControl('', Validators.required);
  motifpres = new FormControl('', Validators.required);

  constructor(private formbuiler: FormBuilder,private dialogRef: MatDialogRef<EditQuickconsultDialogComponent>, @Inject(MAT_DIALOG_DATA) public editData: any,private requests:RequestsService,private route:Router) { }

  ngOnInit(): void {
    this.quickConsultform = this.formbuiler.group(
      {
        refMed: this.refMed,
        nompres: this.nompres,
        motifpres: this.motifpres
      }
      )


  }

  PostfichePatient()
  {
    
    if (this.quickConsultform.valid) {
      let data:fichepatient_post=
      {
        cin: this.editData.cin,
        refMed: this.refMed.value,
        nomPres: this.nompres.value,
        motifPres: this.motifpres.value,
        datePres:new Date().toJSON()
      }

      this.requests.postFichePatient(data).subscribe(
        res=>{
          alert('quick consult is added');
          this.quickConsultform .reset();
          this.dialogRef.close();
          this.route.navigate(['/consultation']);
        },err=>{
          console.log(err);}
      )

    }
  }



      
      getErrorMessage() {
        if (this.nompres.hasError('required')) {
            return 'You must enter a value';
        }
        return this.nompres.hasError('nompres') ? 'Not a valid nompres' : '';
    }
}
