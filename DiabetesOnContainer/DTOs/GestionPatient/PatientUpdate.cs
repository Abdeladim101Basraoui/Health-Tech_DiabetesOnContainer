using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class PatientUpdate
    {

        [Required]
        [StringLength(20)]
        public string Nom { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string Prenom { get; set; } = null!;

        [Required]
        [MaxLength(1)]
        public string Sexe { get; set; }

        [Required]
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; } = null!;


    }
}
