import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-param-bio-details',
  templateUrl: './param-bio-details.component.html',
  styleUrls: ['./param-bio-details.component.css']
})
export class ParamBioDetailsComponent implements OnInit {

  constructor() { }
  @Input() cinSelected!:string;
  @Input() preIsSelected!: number;

  ngOnInit(): void {


  }

}
