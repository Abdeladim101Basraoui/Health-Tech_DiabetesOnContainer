import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { fichepatient, fichepatient_post, patient_put, patient_Read, question_put} from '../_models/requests_models';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RequestsService {

  // Urls
  private readonly fichePatientUrl:string = 'fichePatients';
 private readonly patientUrl:string = 'patients';
  constructor(private http:HttpClient,private authservice:AuthenticationService) { }


  //---fiche patient
  public getFichePatient():Observable<fichepatient[]>
  {
    return this.http.get<fichepatient[]>(`${environment.baseAPIUrl}/${this.fichePatientUrl}`);
  }

  public postFichePatient(data:fichepatient_post)
  {
    return this.http.post(`${environment.baseAPIUrl}/${this.fichePatientUrl}`,data);
  }

  //add question
  public addQuestion(cin:string,presId:number,data:question_put)
  {
    ///api/FichePatients/Add/Q/{Cin}/PresId
    return this.http.post(`${environment.baseAPIUrl}/${this.fichePatientUrl}/Add/${cin}/ ${presId}`,data);
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

// /put
public putPatient(patientdata:patient_put,cin:string)
{
  return this.http.put(`${environment.baseAPIUrl}/${this.patientUrl}/${cin}`,patientdata)
}

public deletePatient(cin:string)
{
  return this.http.delete(`${environment.baseAPIUrl}/${this.patientUrl}/${cin}`);
}


}
