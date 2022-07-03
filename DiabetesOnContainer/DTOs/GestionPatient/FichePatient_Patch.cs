using DiabetesOnContainer.Models;

namespace DiabetesOnContainer.DTOs.GestionPatient
{
    public class FichePatient_Patch:FichePatient_Create
    {
        public int PrescriptionId { get; set; }
    }
}
