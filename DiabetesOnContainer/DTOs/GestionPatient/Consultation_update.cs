using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FichePatient
{
    public class Consultation_update
    {
        [Required]
        public string EtatDuQuestion { get; set; } = null!;
        [Required]
        public string MedecinNotes { get; set; } = null!;
    }
}
