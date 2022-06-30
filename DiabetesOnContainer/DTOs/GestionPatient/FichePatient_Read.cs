using DiabetesOnContainer.Models;
using System.ComponentModel.DataAnnotations;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class FichePatient_Read: FichePatient_Create
    {
        public int PrescriptionId { get; set; }
        public string patientFullName { get; set; }
    }
}
