using DiabetesOnContainer.Models;
namespace DiabetesOnContainer.DTOs.FicheMed
{
    public class FicheMedical_READ : FicheMedical_CUD
    {
        public int FicheMedId { get; set; }
        public ICollection<Bilan_READ> bilans { get; set; }
        public ICollection<Analysis_READ> analyses { get; set; }
        public ICollection<Traitement_READ> traitements { get; set; }

    }
}
