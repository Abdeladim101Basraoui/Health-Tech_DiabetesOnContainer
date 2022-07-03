using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class FichePatient
    {
        public FichePatient()
        {
            Consultations = new HashSet<Consultation>();
            ExamainMedicals = new HashSet<ExamainMedical>();
        }

        public int PrescriptionId { get; set; }
        public string Cin { get; set; } = null!;
        public string RefMed { get; set; } = null!;
        public string NomPres { get; set; } = null!;
        public string MotifPres { get; set; } = null!;
        public DateTime DatePres { get; set; }

        public virtual Patient CinNavigation { get; set; } = null!;
        public virtual Diabeticien RefMedNavigation { get; set; } = null!;
        public virtual ICollection<Consultation> Consultations { get; set; }
        public virtual ICollection<ExamainMedical> ExamainMedicals { get; set; }
    }
}
