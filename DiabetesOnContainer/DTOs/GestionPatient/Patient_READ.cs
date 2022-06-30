using DiabetesOnContainer.DTOs.Admin;
using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class Patient_READ:Patient_CUD
    {
        public string FullName { get; set; }
    }
}
