using System.ComponentModel.DataAnnotations;

namespace dotnetWebAPI.Model
{
    public class FicheMedical
    {
        [Key]
        public string ID { get; set; }
        [Required]
        //public Dictionary<string, string> analyses { get; set; } = new Dictionary<string, string>();
        public string analyses { get; set; } = "my analyses";
       
        [Required]
        public FichePatient? patient { get; set; }
    }
}
