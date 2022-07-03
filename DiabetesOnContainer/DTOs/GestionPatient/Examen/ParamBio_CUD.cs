using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.FichePatient
{
    public class ParamBio_CUD
    {
        [Required]
        public string NomParam { get; set; }

        [Required]
        public string MesureParam { get; set; }

        [Required]
        public string NoteMedecin { get; set; }

        [Required]
        public int ExamainId { get; set; }

    }
}
