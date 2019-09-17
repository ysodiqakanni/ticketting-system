using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using System.Linq;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private db_a3d3ad_pricingContext context;
        //private db_a3d3ad_pricingContext context;
        public UnitOfWork(db_a3d3ad_pricingContext _context)
        {
            context = _context;
            ClientRepository = new ClientRepository(_context);
            TradeRepository = new TradeRepository(_context);
            ClientNoteRepository = new ClientNoteRepository(_context);
            ExchangeRepository = new ExchangeRepository(_context);
            StaffRepository = new StaffRepository(_context);
        }
        public IBaseRepository<UserDetails> ClientRepository { get; set; }
        public IBaseRepository<Trade> TradeRepository { get; set; }
        public IBaseRepository<ClientNote> ClientNoteRepository { get; set; }
        public IBaseRepository<Exchange> ExchangeRepository { get; set; }
        public IBaseRepository<Staff> StaffRepository { get; set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        } 
    }
}
