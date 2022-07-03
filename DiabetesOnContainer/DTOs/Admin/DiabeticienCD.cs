using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.Admin
{
    public class DiabeticienCD : Personne
    {

        [Required]
        public string RefMed { get; set; } = null!;

        [Required]
        //[EmailAddress]
        public string Email { get; set; }

    }


}
