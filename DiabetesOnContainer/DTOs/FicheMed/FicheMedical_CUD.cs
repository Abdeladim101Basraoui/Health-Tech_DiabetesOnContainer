using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FicheMed
{
    public class FicheMedical_CUD
    {
        [Required]
        public string PatientId { get; set; } = null!;
        [Required]
        public string RefMed { get; set; } = null!;
    }
}
