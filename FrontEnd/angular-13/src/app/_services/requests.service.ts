import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { fichepatient, patient_Read} from '../_models/requests_models';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RequestsService {

  // Urls
  private readonly fichePatientUrl:string = 'fichePatients';
 private readonly patientUrl:string = 'patients';
  constructor(private http:HttpClient,private authservice:AuthenticationService) { }


  public getFichePatient():Observable<fichepatient[]>
  {
    return this.http.get<fichepatient[]>(`${environment.baseAPIUrl}/${this.fichePatientUrl}`);
  }


  //---patient
  public getpatients():Observable<patient_Read[]>
  {
   return  this.http.get<patient_Read[]>(`${environment.baseAPIUrl}/${this.patientUrl}`);
  }

  //post
public postPatient(patientdata:any)
{
  return this.http.post(`${environment.baseAPIUrl}/${this.patientUrl}`,patientdata)
}

}
