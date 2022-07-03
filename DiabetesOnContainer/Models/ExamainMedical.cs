using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class ExamainMedical
    {
        public ExamainMedical()
        {
            Echographies = new HashSet<Echography>();
            ParamsBios = new HashSet<ParamsBio>();
        }

        public int ExamainId { get; set; }
        public string NomDiagnostic { get; set; } = null!;
        public string NoteMedecin { get; set; } = null!;
        public byte[]? ImageDiagnositc { get; set; }
        public int PrescriptionId { get; set; }

        public virtual FichePatient Prescription { get; set; } = null!;
        public virtual ICollection<Echography> Echographies { get; set; }
        public virtual ICollection<ParamsBio> ParamsBios { get; set; }
    }
}
