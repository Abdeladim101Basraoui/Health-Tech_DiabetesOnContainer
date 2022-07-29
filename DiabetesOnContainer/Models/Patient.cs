using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Patient
    {
        public Patient()
        {
            CasComplications = new HashSet<CasComplication>();
            FicheMedicals = new HashSet<FicheMedical>();
            FichePatients = new HashSet<FichePatient>();
            Historiques = new HashSet<Historique>();
        }

        public string Cin { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; } = null!;
        public string Sexe { get; set; } = null!;

        public virtual ICollection<CasComplication> CasComplications { get; set; }
        public virtual ICollection<FicheMedical> FicheMedicals { get; set; }
        public virtual ICollection<FichePatient> FichePatients { get; set; }
        public virtual ICollection<Historique> Historiques { get; set; }
    }
}
