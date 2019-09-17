using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TickettingSystem.Core;

namespace TickettingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientNote> ClientNotes { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //public int MyProperty { get; set; }
    }
}
