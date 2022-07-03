using DiabetesOnContainer.Models;
namespace DiabetesOnContainer.DTOs.FicheMed
{
    public class Bilan_READ:Bilan_CUD
    {
        public int BilanId { get; set; }
        public virtual FicheMedical_CUD FicheMedical { get; set; }

    }
}
