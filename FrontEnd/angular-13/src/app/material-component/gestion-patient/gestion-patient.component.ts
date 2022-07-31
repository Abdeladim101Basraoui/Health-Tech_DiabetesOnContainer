import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { tap } from 'rxjs/operators';
import { fichepatient,patient_Read } from 'src/app/_models/requests_models';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { RequestsService } from 'src/app/_services/requests.service';
// import { CRUDService } from 'src/app/_services/crud.service';

@Component({
  selector: 'app-gestion-patient',
  templateUrl: './gestion-patient.component.html',
  styleUrls: ['./gestion-patient.component.css']
})

export class GestionPatientComponent implements OnInit{
  ELEMENT_DATA!:patient_Read[];
  displayedColumns: string[] = ['cin', 'Full Name', 'nom', 'Prenom', 'Gender', 'DateNaissance', 'Email', 'actions'];
  dataSource = new  MatTableDataSource<patient_Read>(this.ELEMENT_DATA);


  constructor(
    private requests: RequestsService,public authservice:AuthenticationService
  ) {

  }

  showPatients() {
    console.log("evetn");
    this.requests.getpatients().subscribe(
      (response) => {
            //map data to data source
        this.dataSource.data = response as patient_Read[];
        console.log(response as patient_Read[])
     },
      (err) => {
        console.log(err);
      }
    )
  }

//add patients
addPatient()
{
  console.log('add');
  
} 


//open dialog
openPatientEdit()
{
    
}

  // ----[pagnation and table properties]

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  //init methods
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

    ngOnInit(): void {
    this.showPatients();
    console.log(this.showPatients());
    
  }



  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}


