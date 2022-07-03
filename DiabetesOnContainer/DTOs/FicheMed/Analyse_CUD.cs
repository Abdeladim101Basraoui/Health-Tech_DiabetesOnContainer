using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FicheMed
{
    public class Analyse_CUD
    {
        [Required]
        public string NomAnalyse { get; set; } = null!;
        [Required]
        public string ResulatAnalyse { get; set; } = null!;
        
        [Required]
        public string NoteMedecin { get; set; } = null!;
        public byte[]? AnalyseImage { get; set; }
      
        [Required]
        public int FicheMedtId { get; set; }

        [Required]
        public DateTime AnalyseDate { get; set; }=DateTime.Now;
    }
}
