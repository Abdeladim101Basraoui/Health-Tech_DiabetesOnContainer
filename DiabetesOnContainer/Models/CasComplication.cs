using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class CasComplication
    {
        public int ComplicationId { get; set; }
        public string ComplicationName { get; set; } = null!;
        public string NoteMedecin { get; set; } = null!;
        public DateTime DateComplication { get; set; }
        public string PlaceUrgence { get; set; } = null!;
        public string PatientId { get; set; } = null!;

        public virtual Patient Patient { get; set; } = null!;
    }
}
