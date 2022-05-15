using Microsoft.EntityFrameworkCore;
using Domain;
namespace CommunicationAppApi
{
    public class RegistersContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=Registers;user=root;password=SagiShoval";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table

            modelBuilder.Entity<Registers>().HasKey(e => e.id);
        }
        public DbSet<Registers>? Registers { get; set; }
    }
}

