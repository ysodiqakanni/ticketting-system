using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TickettingSystem.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TicketingDb;Data Source=.";
            optionsBuilder.UseSqlServer(connection);
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
