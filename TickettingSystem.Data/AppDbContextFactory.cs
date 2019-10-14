//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
////using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
using TickettingSystem.Data.DbModel;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TickettingSystem.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<db_a3d3ad_pricingContext>
    {
        public db_a3d3ad_pricingContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<db_a3d3ad_pricingContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<db_a3d3ad_pricingContext>();
            //var connection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TicketingDb;Data Source=.";
            //var connection = @"server=localhost;port=3306;user=root;password=root;database=ticketing_db";

            optionsBuilder.UseMySql(connectionString);
            return new db_a3d3ad_pricingContext(optionsBuilder.Options);
        }
    }
}
