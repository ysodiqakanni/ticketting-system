using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<UserDetails> ClientRepository { get; set; }
        IBaseRepository<Trade> TradeRepository { get; set; }
        IBaseRepository<ClientNote> ClientNoteRepository { get; set; }
        IBaseRepository<Exchange> ExchangeRepository { get; set; }
        IBaseRepository<Staff> StaffRepository { get; set; }
        int Save();
    }
}
