import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { fichepatient, questions } from 'src/app/_models/requests_models';
import { RequestsService } from 'src/app/_services/requests.service';

@Component({
  selector: 'app-questions-details',
  templateUrl: './questions-details.component.html',
  styleUrls: ['./questions-details.component.css']
})
export class QuestionsDetailsComponent implements OnInit {

  ELEMENT_DATA!: questions[];
  dataSource = new MatTableDataSource<questions>(this.ELEMENT_DATA);

  constructor(private reqService: RequestsService) { }

  @Input() cinSelected!: string;
  @Input() preIsSelected!: number


  ngOnInit(): void {
    this.getQuestions();
  }


  getQuestions() {
    this.reqService.getFichePatient().subscribe(
      req => {
        console.log(req);
        console.log(this.preIsSelected);
        this.ELEMENT_DATA = req[this.preIsSelected].questions;
        console.log(req[0].questions);
      }, err => {

      }

    )

  }

}
