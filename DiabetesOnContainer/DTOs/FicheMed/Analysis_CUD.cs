using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FicheMed
{
    public class Analysis_CUD
    {
        [Required]
        public string NomAnalyse { get; set; } = null!;
        [Required]
        public string ResulatAnalyse { get; set; } = null!;
        
        [Required]
        public string NoteMedecin { get; set; } = null!;
        public byte[]? AnalyseImage { get; set; }

        [Required]
        public int FicheMedId { get; set; }

        [Required]
        public DateTime DateEnvoi { get; set; }=DateTime.Now;
    }
}
