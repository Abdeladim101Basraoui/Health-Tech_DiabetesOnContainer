
using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FichePatient
{
    public class Question_CUD
    {

        [Required]
        public string Question1 { get; set; } = null!;

    }
}
