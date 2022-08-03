import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-echographie-details',
  templateUrl: './echographie-details.component.html',
  styleUrls: ['./echographie-details.component.css']
})
export class EchographieDetailsComponent implements OnInit {

  constructor() { }
  @Input() cinSelected!:string;
  @Input() preIsSelected!: number;

  ngOnInit(): void {

    
  }

}
