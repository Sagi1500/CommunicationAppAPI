using Domain;
using Microsoft.EntityFrameworkCore;


namespace CommunicationApi.Data
{

    public class AppContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=AppDB;user=root;password=SagiShoval";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            modelBuilder.Entity<User>().HasKey(u => u.id);
            modelBuilder.Entity<Contact>().HasKey(c => new { c.id, c.name });
            modelBuilder.Entity<Message>().HasKey(m => m.id);
            modelBuilder.Entity<Contact>().HasMany<Message>(c=> c.messagesList).WithOne(m => m.contact).HasForeignKey(m => m.id); ;
            modelBuilder.Entity<User>().HasMany<Contact>(u => u.contactsList).WithOne(u => u.user).HasForeignKey(c => new { c.id, c.name });          
        }

        public DbSet<User>? Users { get; set; }

        public DbSet<Contact>? Contacts { get; set; }

        public DbSet<Message>? Messages { get; set; }
    }

}
