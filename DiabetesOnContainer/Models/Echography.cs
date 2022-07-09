using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiabetesOnContainer.Models
{
    public partial class Echography
    {
        public int EchographieId { get; set; }
        public string NomEchographie { get; set; } = null!;
        public string NoteMedecin { get; set; } = null!;
        public byte[]? ImageEchographie { get; set; }
        public string? AutrePathology { get; set; }
        public int ExamainId { get; set; }
        [JsonIgnore]
        public virtual ExamainMedical Examain { get; set; } = null!;
    }
}
