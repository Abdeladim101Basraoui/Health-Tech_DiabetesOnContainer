import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-consult-details',
  templateUrl: './consult-details.component.html',
  styleUrls: ['./consult-details.component.css']
})
export class ConsultDetailsComponent implements OnInit {

  //cin selected
  cinSelected!: string;
  preIdSelected!:number;

  constructor(private router: Router) {

    this.cinSelected = this.router.getCurrentNavigation()?.extras.state?.cin;
    this.preIdSelected = this.router.getCurrentNavigation()?.extras.state?.presId;
    
    if (this.cinSelected == undefined) {
      this.router.navigate(['/consultation'])
      console.log('the sent cin is empty');

    }
  }

  ngOnInit(): void {

  }

}
