using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
//using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class ClientRepository : BaseRepository<UserDetails>, IClientRepository
    {
        //db_a3d3ad_pricingContext _ctx;
      
        public ClientRepository(DbContext ctx) : base(ctx)
        {
            //_ctx = ctx as db_a3d3ad_pricingContext;
        }
        //public void UpdateClient(UserDetails client)
        //{
          
        //}

    }
}
