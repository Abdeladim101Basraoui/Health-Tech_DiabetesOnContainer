using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class FicheMedical
    {
        public FicheMedical()
        {
            Analyses = new HashSet<Analysis>();
            Bilans = new HashSet<Bilan>();
            Traitements = new HashSet<Traitement>();
        }

        public int FicheMedId { get; set; }
        public string PatientId { get; set; } = null!;
        public string RefMed { get; set; } = null!;

        public virtual Patient Patient { get; set; } = null!;
        public virtual Diabeticien RefMedNavigation { get; set; } = null!;
        public virtual ICollection<Analysis> Analyses { get; set; }
        public virtual ICollection<Bilan> Bilans { get; set; }
        public virtual ICollection<Traitement> Traitements { get; set; }
    }
}
