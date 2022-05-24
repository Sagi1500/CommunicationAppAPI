using Domain;
using Microsoft.EntityFrameworkCore;


namespace CommunicationApi.Data
{

    public class ApplicationContext : DbContext
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
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasMany(u => u.ContactsList).WithOne(c => c.User).HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Contact>().HasKey(c => new { c.Id, c.UserId });
            modelBuilder.Entity<Contact>().HasOne(c => c.User).WithMany(u => u.ContactsList).HasForeignKey(c => c.UserId);
            modelBuilder.Entity<Contact>().HasMany(c => c.MessagesList).WithOne(m => m.Contact).HasForeignKey(m => new { m.ContactId, m.UserId });

            modelBuilder.Entity<Message>().HasKey(m => m.Id);
            modelBuilder.Entity<Message>().HasOne(m => m.Contact).WithMany(c => c.MessagesList).HasForeignKey(m => new { m.ContactId, m.UserId });
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Message> Messages { get; set; }
    }

}
