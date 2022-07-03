using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FicheMed
{
    public class Traitement_CUD
    {
        [Required]
        public int FicheMedId { get; set; }

        [Required]
        public string NomTraitement { get; set; } = null!;

        [Required]
        public string NoteMedecin { get; set; } = null!;
        
        [Required]
        public DateTime DateTrait { get; set; }= DateTime.Now;
        
        [Required]
        public DateTime? DateFinTrait { get; set; }
        
        [Required]
        public DateTime DateEnvoi { get; set; }=DateTime.Now;

    }
}
