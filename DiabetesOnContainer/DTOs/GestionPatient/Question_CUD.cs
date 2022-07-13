
using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class Question_CUD
    {

        [Required]
        public string Question1 { get; set; } = null!;
        [Required]
        public string EtatDuQuestion { get; set; } = null!;
        [Required]
        public string MedecinNotes { get; set; } = null!;

    }
}
