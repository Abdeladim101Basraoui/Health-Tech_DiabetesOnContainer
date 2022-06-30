﻿using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.Admin
{
    public class Assist_Update
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
        //[EmailAddress]
        public string Email { get; set; }
    }
}
