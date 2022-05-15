using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

    public class RegistersContext : DbContext
    {
        public RegistersContext (DbContextOptions<RegistersContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Registers>? Registers { get; set; }
    }
