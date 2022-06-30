using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class ExamenMed_Update
    {
        [Required]
        public string NomDiagnostic { get; set; } = null!;

        [Required]
        public string NoteMedecin { get; set; } = null!;


        public byte[]? ImageDiagnositc { get; set; }
    }
}
