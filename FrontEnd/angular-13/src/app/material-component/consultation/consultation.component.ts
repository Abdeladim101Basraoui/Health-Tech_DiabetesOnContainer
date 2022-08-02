import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { fichepatient } from 'src/app/_models/requests_models';
import { RequestsService } from 'src/app/_services/requests.service';

@Component({
  selector: 'app-consultation',
  templateUrl: './consultation.component.html',
  styleUrls: ['./consultation.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ConsultationComponent implements OnInit {

  ELEMENT_DATA!:fichepatient[];
  columnsToDisplay = ["prescriptionId", 'cin', 'patientFullName', 'nomPres','datePres'];
  //'motif Presciption'
  columnsToDisplayWithExpand = [...this.columnsToDisplay ,'actions','expand'];
  dataSource = new  MatTableDataSource<fichepatient>(this.ELEMENT_DATA);
  expandedElement!: fichepatient | null;


  constructor(private requestservice:RequestsService) { }

  ngOnInit(): void {
    this.showFichePatient();
  }


  //show data
  showFichePatient()
  {
    this.requestservice.getFichePatient().subscribe(
      res=>{
        this.dataSource.data = res as fichepatient[];
      },err=>{
        console.log(err);
        
      }
    );
  }

}
