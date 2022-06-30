using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FichePatient
{
    public class Historique_CUD
    {
        [Required]
        public string Type { get; set; }
        
        [Required]
        public string? NoteMedecin { get; set; }

        [Required]
        public DateTime DateHistorique { get; set; }=DateTime.Now;

        public byte[]? HistoriqueImage { get; set; }

        [Required]
        public string PatientId { get; set; }
    }
}
