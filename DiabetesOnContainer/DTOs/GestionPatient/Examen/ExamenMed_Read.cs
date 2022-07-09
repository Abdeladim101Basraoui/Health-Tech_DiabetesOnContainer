namespace DiabetesOnContainer.DTOs.GestionPatient

{
    public class ExamenMed_Read : ExamenMed_CD
    {
        public int ExamainId { get; set; }
        public ICollection<Echographie_READ> echographies { get; set; }
        public ICollection<ParamBio_Read> paramBio { get; set; }    
    }
}
