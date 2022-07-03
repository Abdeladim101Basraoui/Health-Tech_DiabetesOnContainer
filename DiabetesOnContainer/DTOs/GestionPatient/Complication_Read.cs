using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class Complication_Read:Complication_CUD
    {
        [Required]
        public int ComplicationId { get; set; }
      
    }
}
