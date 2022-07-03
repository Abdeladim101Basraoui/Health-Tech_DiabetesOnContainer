using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class ParamBio_CD:ParamBio_Update
    {
        [Required]
        public int ExamainId { get; set; }

    }
}
