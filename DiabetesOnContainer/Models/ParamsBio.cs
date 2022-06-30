using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class ParamsBio
    {
        public int ParamBioId { get; set; }
        public string NomParam { get; set; } = null!;
        public string MesureParam { get; set; } = null!;
        public string NoteMedecin { get; set; } = null!;
        public int ExamainId { get; set; }

        public virtual ExamainMedical Examain { get; set; } = null!;
    }
}
