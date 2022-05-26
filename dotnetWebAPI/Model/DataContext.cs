
using Microsoft.EntityFrameworkCore;

namespace dotnetWebAPI.Model
{
    public class DataContext:DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 

        }
        public DbSet<FichePatient> Patients { get; set; }

        public DbSet<FicheMedical> ficheMedicals { get; set; }
    }
}
