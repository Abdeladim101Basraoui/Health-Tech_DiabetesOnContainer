using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.Admin
{
    public class AssistCD :Personne
    {
 
        [Required]
        //[EmailAddress]
        public string Email { get; set; }
     
    }
}
