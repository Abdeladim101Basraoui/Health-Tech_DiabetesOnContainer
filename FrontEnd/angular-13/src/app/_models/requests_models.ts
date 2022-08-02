import { exhaustMap } from "rxjs-compat/operator/exhaustMap"
import { expand } from "rxjs-compat/operator/expand"

//patient put
export class patient_put {
    dateNaissance!: Date
    email!: string
    nom!: string
    prenom!: string
    sexe!: string

}

//patient cud
export class patient_Cud extends patient_put {

    cin!: string

}

//patient read
export class patient_Read extends patient_Cud {
    fullName!: string;
    prescriptionId!:number;
}

//------------------
//
export class fichepatient_post
{
       cin!: string;
    refMed!: string;
    nomPres!: string;
    motifPres!: string;
    datePres!: string;
}

export class fichepatient extends fichepatient_post{
    prescriptionId!: number;
    patientFullName!: string;
    // questions !: questions[];
    // examainMedicals!: exams[];
 
}




// ----
// questions requests
export class questions {
    questionId!: number;
    question1!: string;
    etatDuQuestion!: string;
    medecinNotes!: string;
}




// ------
// exams requests
export class exams {

    examainId!: number;
    // echographies!: echographies[];
    // paramBio!: paramBio[];
    prescriptionId!:number;
    nomDiagnostic!:string;
    noteMedecin!:string;
    imageDiagnositc!:string
}



// -------
// echographies requests 
export class echographies
{
    echographieId!:number;
    nomEchographie!:string;
    noteMedecin!:string;
    imageEchographie!:string;
    autrePathology!:string;
    examainId!:number;
}

// -------
// paramBio requests
export class paramBio
{
 paramBioId!:number;   
 nomParam!:string;   
 mesureParam!:string;   
 noteMedecin!:string;   
 examainId!:number;   
}