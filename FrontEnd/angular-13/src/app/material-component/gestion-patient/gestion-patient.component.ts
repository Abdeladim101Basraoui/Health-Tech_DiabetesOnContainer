import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
// import { CRUDService } from 'src/app/_services/crud.service';

@Component({
  selector: 'app-gestion-patient',
  templateUrl: './gestion-patient.component.html',
  styleUrls: ['./gestion-patient.component.css']
})

export class GestionPatientComponent  {
  displayedColumns: string[] = ['cin', 'Full Name','nom', 'Prenom', 'DateNaissance','Email','Gender','actions'];
  // dataSource: MatTableDataSource<PatientData>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(

    ) {

  }

  ngAfterViewInit() {
    // this.dataSource.paginator = this.paginator;
    // this.dataSource.sort = this.sort;
  }
  // applyFilter(event: Event) {
  //   const filterValue = (event.target as HTMLInputElement).value;
  //   this.dataSource.filter = filterValue.trim().toLowerCase();

  //   if (this.dataSource.paginator) {
  //     this.dataSource.paginator.firstPage();
  //   }
  // }
  getvalues()
{
  // this.crudService.getPatients();
}

}

// /** Builds and returns a new User. */
// function createNewUser(id: number): PatientData {
//   const name =  NAMES[Math.round(Math.random() * (NAMES.length - 1))] +
//     ' ' +
//     NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) +
//     '.';

//   return {
//     CIN: id.toString(),
//     fullname: name,
//     DateNaissance :new Date(),
//     Email : FRUITS[Math.round(Math.random()  * (FRUITS.length -1))],
//     sexe : "f"
//   };






export interface PatientData {
      CIN:string,
      fullname:string
      ,DateNaissance:Date
      ,Email:string
      ,sexe:string
}
/** Constants used to fill up our data base. */
const FRUITS: string[] = [
  'blueberry',
  'lychee',
  'kiwi',
  'mango',
  'peach',
  'lime',
  'pomegranate',
  'pineapple',
];
const NAMES: string[] = [
  'Maia',
  'Asher',
  'Olivia',
  'Atticus',
  'Amelia',
  'Jack',
  'Charlotte',
  'Theodore',
  'Isla',
  'Oliver',
  'Isabella',
  'Jasper',
  'Cora',
  'Levi',
  'Violet',
  'Arthur',
  'Mia',
  'Thomas',
  'Elizabeth',
];