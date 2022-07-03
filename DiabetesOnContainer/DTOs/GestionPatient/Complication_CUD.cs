using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class Complication_CUD
    {
        [Required]
        public string ComplicationName { get; set; }


        [Required(ErrorMessage = "please insert the medecin notes about this particalr complication !!important")]
        public string NoteMedecin { get; set; }

        [Required]
        public DateTime DateComplication { get; set; } = DateTime.Now;

        [Required]
        public string PlaceUrgence { get; set; } = "cabinet AlAmel";

        [Required]
        [MaxLength(10)]
        public string PatientId { get; set; }

    }
}
