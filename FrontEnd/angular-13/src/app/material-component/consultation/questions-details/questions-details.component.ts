import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { fichepatient, questions } from 'src/app/_models/requests_models';
import { RequestsService } from 'src/app/_services/requests.service';
import { QuestionDialogComponent } from './question-dialog/question-dialog.component';

@Component({
  selector: 'app-questions-details',
  templateUrl: './questions-details.component.html',
  styleUrls: ['./questions-details.component.css']
})
export class QuestionsDetailsComponent implements OnInit {

  ELEMENT_DATA!: questions[];
  // dataSource = new MatTableDataSource<questions>(this.ELEMENT_DATA);
  dataSource!: questions[];

  constructor(private reqService: RequestsService, private dialog: MatDialog) { }

  @Input() cinSelected!: string;
  @Input() preIsSelected!: number


  ngOnInit(): void {
    this.getQuestions();
  }


  getQuestions() {
    this.reqService.getFichePatient().subscribe(
      req => {

        console.log(this.preIsSelected);

        for (const element of req) {
          if (element.prescriptionId == this.preIsSelected) {
            // console.log(element.prescriptionId);
          this.dataSource = element.questions;  
            for (let index = 0; index < element.questions.length; index++) {
              console.log(element.questions[index]);  
  
            }

            console.log(this.dataSource);
            
          }
        }

      }, err => {

      }

    )

  }

  // add Question
  addQuestion()
  {
    const dialogRef = this.dialog.open(QuestionDialogComponent,
      {
        width: "50%"
      });

    dialogRef.afterClosed().subscribe(result => {
      if (result == 'save') {
        this.getQuestions();
      }
    });
    
  }
}
