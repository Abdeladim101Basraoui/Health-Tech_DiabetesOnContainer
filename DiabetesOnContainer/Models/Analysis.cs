using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Analysis
    {
        public int AnalyseId { get; set; }
        public string NomAnalyse { get; set; } = null!;
        public string ResulatAnalyse { get; set; } = null!;
        public string NoteMedecin { get; set; } = null!;
        public byte[]? AnalyseImage { get; set; }
        public int FicheMedId { get; set; }
        public DateTime DateEnvoi { get; set; }

        public virtual FicheMedical FicheMed { get; set; } = null!;
    }
}
