using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class Echographie_CD
    {

        [Required]
        public string? NomEchographie { get; set; }
        [Required]
        public string? NoteMedecin { get; set; }
        public byte[]? ImageEchographie { get; set; }
        public string? AutrePathology { get; set; }
        public int ExamainId { get; set; }
    }
}
