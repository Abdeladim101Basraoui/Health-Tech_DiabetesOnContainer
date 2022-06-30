using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FicheMed
{
    public class Bilan_CUD
    {
        [Required]
        public string NomBilan { get; set; } = null!;
        [Required]
        public string ResulatBilan { get; set; } = null!;
        
        [Required]
        public string NoteMedecin { get; set; } = null!;
        public byte[]? BilanImage { get; set; }
      
        [Required]
        public int FicheMedtId { get; set; }
        [Required]
        public DateTime BilanDate { get; set; }=DateTime.Now;
    }
}
