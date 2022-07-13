using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.Admin.log_In_Out
{
    public class Doclogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
