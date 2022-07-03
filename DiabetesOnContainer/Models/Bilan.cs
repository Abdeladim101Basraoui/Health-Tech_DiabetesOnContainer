using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Bilan
    {
        public int BilanId { get; set; }
        public string NomBilan { get; set; } = null!;
        public string ResulatBilan { get; set; } = null!;
        public string NoteMedecin { get; set; } = null!;
        public byte[]? BilanImage { get; set; }
        public int FicheMedId { get; set; }
        public DateTime DateEnvoi { get; set; }

        public virtual FicheMedical FicheMed { get; set; } = null!;
    }
}
