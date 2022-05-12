using CommunicationAppApi.Models;
using Microsoft.EntityFrameworkCore;


namespace CommunicationAppApi
{
    public class UsersContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=Users;user=root;password=SagiShoval";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            modelBuilder.Entity<User>().HasKey(e => e.id);
        }

        //public DbSet<Rating>? Ratings { get; set; }
        //public DbSet<Contact>? Contacts { get; set; }

        public DbSet<User>? Users { get; set; }



    }
}
