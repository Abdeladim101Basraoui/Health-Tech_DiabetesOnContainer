using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.Admin.log_In_Out
{
    public class DocRegister:DiabeticienCD
    {
        [Required]
        public string password { get; set; }
    }
}
