using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Diabeticien
    {
        public Diabeticien()
        {
            FicheMedicals = new HashSet<FicheMedical>();
            FichePatients = new HashSet<FichePatient>();
        }

        public string Cin { get; set; } = null!;
        public string RefMed { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Sexe { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public virtual ICollection<FicheMedical> FicheMedicals { get; set; }
        public virtual ICollection<FichePatient> FichePatients { get; set; }
    }
}
