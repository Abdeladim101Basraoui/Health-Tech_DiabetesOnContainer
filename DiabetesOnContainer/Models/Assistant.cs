using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class Assistant
    {
        public Assistant()
        {
            Patients = new HashSet<Patient>();
        }

        public string Cin { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Sexe { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
