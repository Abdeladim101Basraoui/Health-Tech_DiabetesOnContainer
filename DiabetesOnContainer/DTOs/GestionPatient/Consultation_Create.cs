using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{ 
    public class Consultation_Create
    {
        [Required]
        public int PrescriptionId { get; set; }
        [Required]
        public int QuestionId { get; set; }

    }
}
