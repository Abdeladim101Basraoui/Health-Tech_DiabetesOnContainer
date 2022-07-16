using DiabetesOnContainer.DTOs.Admin;
using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class Patient_READ 
    {
        [Required]
        public string Cin { get; set; }

        public string FullName { get; set; }
        [Required]
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; } = null!;
        [Required]
        public string Sexe { get; set; }


    }
}
