using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FichePatient
{ 
    public class Consultation_Create:Consultation_update
    {
        [Required]
        public int PrescriptionId { get; set; }
        [Required]
        public int QuestionId { get; set; }

    }
}
