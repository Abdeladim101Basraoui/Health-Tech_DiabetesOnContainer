import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-questions-details',
  templateUrl: './questions-details.component.html',
  styleUrls: ['./questions-details.component.css']
})
export class QuestionsDetailsComponent implements OnInit {

  constructor() { }
  @Input() cinSelected!:string;

  ngOnInit(): void {


  }

}
