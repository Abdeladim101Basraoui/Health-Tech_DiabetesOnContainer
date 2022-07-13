using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Question
    {
        public Question()
        {
            Prescriptions = new HashSet<FichePatient>();
        }

        public int QuestionId { get; set; }
        public string Question1 { get; set; } = null!;
        public string EtatDuQuestion { get; set; } = null!;
        public string MedecinNotes { get; set; } = null!;

        public virtual ICollection<FichePatient> Prescriptions { get; set; }
    }
}
