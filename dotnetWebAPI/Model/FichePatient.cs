using System.ComponentModel.DataAnnotations;

namespace dotnetWebAPI.Model
{
    public class FichePatient
    {
        [Key]
        public int C_ID { get; set; }

        [Required]
        public string C_Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string C_lastName { get; set; }


        public FichePatient(string name,string lastname)
        {
            this.C_Name = name;
            this.C_lastName = lastname;
        }
    }
}
