
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: []
})
export class AppHeaderComponent {
logOut()
{
localStorage.clear();
this.route.navigate(['login'])
}
/**
 *
 */
constructor(public route:Router) {

}

}
