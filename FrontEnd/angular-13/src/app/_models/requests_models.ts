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
    fullName!: string
}
