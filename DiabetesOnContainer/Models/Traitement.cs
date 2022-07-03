using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Traitement
    {
        public int TraitId { get; set; }
        public int FicheMedId { get; set; }
        public string NomTraitement { get; set; } = null!;
        public string NoteMedecin { get; set; } = null!;
        public DateTime DateTrait { get; set; }
        public DateTime? DateFinTrait { get; set; }
        public DateTime DateEnvoi { get; set; }

        public virtual FicheMedical FicheMed { get; set; } = null!;
    }
}
