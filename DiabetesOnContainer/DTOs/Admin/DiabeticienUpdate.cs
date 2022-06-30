using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.Admin
{
    public class DiabeticienUpdate
    {
        [Required]
        [StringLength(20)]
        public string Nom { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string Prenom { get; set; } = null!;

        [Required]
        [MaxLength(3)]
        public string Sexe { get; set; }
        private string _email;
        [Required]
        //[EmailAddress]
        public string Email { get => _email; set => _email = value; }
    }
}
