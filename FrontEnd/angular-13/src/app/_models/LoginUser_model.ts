export class loginUser {
  role !: string;
  email !: string;
  password !: string;
}
export interface Roles {
  value: string;
  viewValue: string;
}

export class RegisterAssist {
  email !: string;
  password !: string;
  cin!: string;
  nom!: string;
  prenom!: string;
  sexe!: string;
}

export class RegisterDoc extends RegisterAssist {
  RefMed!: string
}

