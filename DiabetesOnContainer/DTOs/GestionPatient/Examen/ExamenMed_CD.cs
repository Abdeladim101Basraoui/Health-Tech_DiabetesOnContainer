using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{ 
    public class ExamenMed_CD:ExamenMed_Update
    {
     
        [Required]
        public int PrescriptionId { get; set; }
    }
}
