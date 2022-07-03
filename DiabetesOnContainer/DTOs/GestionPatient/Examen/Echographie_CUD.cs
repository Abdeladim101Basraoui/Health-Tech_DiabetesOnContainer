namespace DiabetesOnContainer.DTOs.FichePatient
{
    public class Echographie_CUD
    {

        public string? NomEchographie { get; set; }
        public string? NoteMedecin { get; set; }
        public byte[]? RienImage { get; set; }
        public string? AutrePathology { get; set; }
        public int ExamainId { get; set; }
    }
}
