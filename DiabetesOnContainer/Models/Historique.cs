using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Historique
    {
        public int HistoriqueId { get; set; }
        public string Type { get; set; } = null!;
        public string NoteMedecin { get; set; } = null!;
        public DateTime DateHistorique { get; set; }
        public byte[]? HistoriqueImage { get; set; }
        public string PatientId { get; set; } = null!;

        public virtual Patient Patient { get; set; } = null!;
    }
}
