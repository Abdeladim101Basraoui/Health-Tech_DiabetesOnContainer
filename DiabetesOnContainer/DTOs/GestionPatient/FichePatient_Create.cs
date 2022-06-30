using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class FichePatient_Create
    {
        [Required]
        public string? Cin { get; set; } = null!;
        [Required]
        public string? RefMed { get; set; } = null!;
        [Required]
        public string? NomPres { get; set; }
        [Required]
        public string? MotifPres { get; set; }
        [Required]
        public DateTime DatePres { get; set; } = DateTime.Now;
    }
}
