using Microsoft.EntityFrameworkCore;
using CommunicationAppApi.Models;

public class UsersContext : DbContext
{
    private const string connectionString = "server=localhost;port=3306;database=Users;user=root;password=SagiShoval";
    public UsersContext(DbContextOptions<DbContextOptions<UsersContext>> option)
        :base(options)
    { }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<User> Users { get; set; }
}
