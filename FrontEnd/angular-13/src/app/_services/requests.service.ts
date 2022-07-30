import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RequestsService {

  constructor(private http:HttpClient,private authservice:AuthenticationService) { }


  public getPatient()
  {
    
  }

}
