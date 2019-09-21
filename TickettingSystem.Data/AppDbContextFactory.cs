using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<db_a3d3ad_pricingContext>
    {
        public db_a3d3ad_pricingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<db_a3d3ad_pricingContext>();
            //var connection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TicketingDb;Data Source=.";
            var connection = @"server=localhost;port=3306;user=root;password=root;database=ticketing_db";
            optionsBuilder.UseMySql(connection);
            return new db_a3d3ad_pricingContext(optionsBuilder.Options);
        }
    }
}
