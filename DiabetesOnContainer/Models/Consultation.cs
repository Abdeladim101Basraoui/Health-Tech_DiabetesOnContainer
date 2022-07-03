using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Consultation
    {
        public int PrescriptionId { get; set; }
        public int QuestionId { get; set; }
        public string EtatDuQuestion { get; set; } = null!;
        public string MedecinNotes { get; set; } = null!;

        public virtual FichePatient Prescription { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
    }
}
