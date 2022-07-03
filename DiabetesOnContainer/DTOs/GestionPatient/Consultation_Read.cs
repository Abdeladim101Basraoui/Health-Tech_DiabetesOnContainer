using System.ComponentModel.DataAnnotations;
namespace DiabetesOnContainer.DTOs.FichePatient
{
    public class Consultation_Read:Consultation_Create
    {
        public string QuestionIS { get; set; } = String.Empty;
    }
}
