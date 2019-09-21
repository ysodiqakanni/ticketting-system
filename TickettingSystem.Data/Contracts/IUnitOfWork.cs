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
        IBaseRepository<TradeLog> TradeRepository { get; set; }
        IBaseRepository<UserNotes> ClientNoteRepository { get; set; }
        IBaseRepository<Exchangesusers> ExchangeRepository { get; set; }
        IBaseRepository<ExchangeType> ExchangeTypeRepository { get; set; }
        IBaseRepository<StaffDetails> StaffRepository { get; set; }
        IBaseRepository<Languages> LanguageRepository { get; set; }
        IBaseRepository<UserVerification> UserVerificationRepository { get; set; }
        IBaseRepository<SupportTicket> TicketRepository { get; set; }
        IBaseRepository<Departments> DepartmentRepository { get; set; }
        IBaseRepository<StaffNotes> StaffNoteRepository { get; set; }


        int Save();
    }
}
