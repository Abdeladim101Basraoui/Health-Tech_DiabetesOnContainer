using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Question
    {
        public Question()
        {
            Consultations = new HashSet<Consultation>();
        }

        public int QuestionId { get; set; }
        public string Question1 { get; set; } = null!;

        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
