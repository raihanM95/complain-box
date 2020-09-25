using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainBox.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserAccount> userAccounts { get; set; }

        public DbSet<Complain> complains { get; set; }

        public DbSet<Note> notes { get; set; }

        public DbSet<Appointment> appointments { get; set; }
    }
}
