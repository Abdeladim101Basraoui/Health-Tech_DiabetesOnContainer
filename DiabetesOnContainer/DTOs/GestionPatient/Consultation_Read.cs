using System.ComponentModel.DataAnnotations;
namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class Consultation_Read:Consultation_Create
    {
        public ICollection<Question_READ> Questions{ get; set; }
    }
}
