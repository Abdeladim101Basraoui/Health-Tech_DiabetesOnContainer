using DiabetesOnContainer.DTOs.Admin;
using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class Patient_CUD:Personne
    {

        [Required]
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; } = null!;

    }
}
