using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TickettingSystem.Data;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Data.Implementations;
using TickettingSystem.Services.Contracts;
using TickettingSystem.Services.Implementations;

namespace TickettingSystem.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // changed the Mysql provider https://stackoverflow.com/a/50868381/7162741
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<db_a3d3ad_pricingContext>(options =>
                    options.UseMySql(connectionString)
            );


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IClientNoteService, ClientNoteService>();
            services.AddTransient<IExchangeService, ExchangeService>();
            services.AddTransient<ITradeService, TradeService>();
            services.AddTransient<ITicketService, TicketService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
