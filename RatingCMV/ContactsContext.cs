
//using Microsoft.EntityFrameworkCore;
//using CommunicationAppApi.Models;

//    public class ContactsContext : DbContext
//    {
//        private const string connectionString = "server=localhost;port=3306;database=Contacts;user=root;password=SagiShoval";

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Configuring the Name property as the primary
//            // key of the Items table
//            modelBuilder.Entity<Contact>().HasKey(e => e.id);
//        }

//    public DbSet<Contact>? Contacts { get; set; }
//    }

