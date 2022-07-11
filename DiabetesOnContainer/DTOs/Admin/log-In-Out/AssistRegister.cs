using DiabetesOnContainer.Models;
using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.Admin.log_In_Out
{
    public class AssistRegister : AssistCD
    {
        [Required]
        public string password { get; set; }
    }
}
