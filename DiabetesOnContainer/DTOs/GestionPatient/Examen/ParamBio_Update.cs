using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class ParamBio_Update
    {
        [Required]
        public string NomParam { get; set; } = null!;
        [Required]
        public string MesureParam { get; set; } = null!;
        [Required]
        public string NoteMedecin { get; set; } = null!;
    }
}
