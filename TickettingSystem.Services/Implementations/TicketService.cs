using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Services.Implementations
{
    public class TicketService : ITicketService
    {
        IUnitOfWork uow;
        IClientService clientService;
        public TicketService(IUnitOfWork _uow, IClientService _clientService)
        {
            uow = _uow;
            clientService = _clientService;
        }
        public List<SupportTicket> GetTop10()
        {
            // get only the parent tickets
            var top10 = uow.TicketRepository.QueryAll().Where(t => t.ParentTicketId == null).Take(10);
            return top10.ToList();
        }
        public List<SupportTicket> GetParentTicketsByClientId(int id)
        {
            // get only the parent tickets
            var top10 = uow.TicketRepository.QueryAll().Where(t => t.UserId == id && t.ParentTicketId == null).Take(10);
            return top10.ToList();
        }
        public List<SupportTicket> GetParentTicketsByClientName(string name)
        {
            // get clients whose name ends with "name" and return their ids
            // get only the parent tickets
            var clientIds = clientService.GetIdsOfClientsWithNamesContain(name);
            var top10 = uow.TicketRepository.QueryAll().Where(t => clientIds.Contains(t.UserId) && t.ParentTicketId == null).Take(10);
            return top10.ToList();
        }
        public List<SupportTicket> GetParentTicketsByClientNamePrefix(string name)
        {
            // get clients whose name ends with "name" and return their ids
            // get only the parent tickets
            var clientIds = clientService.GetIdsOfClientsWithNamesEnd(name);
            var top10 = uow.TicketRepository.QueryAll().Where(t => clientIds.Contains(t.UserId) && t.ParentTicketId == null).Take(10);
            return top10.ToList();
        }
        public List<SupportTicket> GetParentTicketsByClientNameSuffix(string name)
        {
            // get clients whose name starts with "name" and return their ids
            // get only the parent tickets
            var clientIds = clientService.GetIdsOfClientsWithNamesStart(name);
            var top10 = uow.TicketRepository.QueryAll().Where(t => clientIds.Contains(t.UserId) && t.ParentTicketId == null).Take(10);
            return top10.ToList();
        }
        public List<SupportTicket> GetParentTicketsByClientIdPrefix(string id)
        { 
            var top10 = uow.TicketRepository.QueryAll().Where(t => t.UserId.ToString().EndsWith(id) && t.ParentTicketId == null).Take(10);
            return top10.ToList();
        }
        public List<SupportTicket> GetParentTicketsByClientIdSuffix(string id)
        {
            var top10 = uow.TicketRepository.QueryAll().Where(t => t.UserId.ToString().StartsWith(id) && t.ParentTicketId == null).Take(10);
            return top10.ToList();
        }
        public List<SupportTicket> GetConversationsForParentTicket(int parentTicketId)
        { 
            var tickets = uow.TicketRepository.QueryAll().Where(t => t.ParentTicketId == parentTicketId);
            return tickets.ToList();
        }
        //public List<SupportTicket> GetParentTicketsByClientId(int id)
        //{
        //    return uow.TicketRepository.QueryAll().Where(t => t.UserId == id).ToList();
        //}
        public SupportTicket CloseTicket(int id)
        {
            var ticket = uow.TicketRepository.Get(id);
            if(ticket == null)
            {
                throw new Exception("Ticket not found!");
            }
            ticket.DtClosed = DateTime.Now;
            uow.Save();
            return ticket;
        }
        public SupportTicket Update(int id, int staffId, string note)
        {
            var ticket = uow.TicketRepository.Get(id);
            if (ticket == null)
            {
                throw new Exception("Ticket not found!");
            }
            ticket.AssignedTo = staffId;

            // save new note as a ticket
            var newTicket = new SupportTicket
            {
                Message = note,
                Subject = "Notes by staff",
                ParentTicketId = id,
                DtCreated = DateTime.Now,
                DtUpdated = DateTime.Now,
                UserId = ticket.UserId
            };
            uow.TicketRepository.Add(newTicket);

            uow.Save();
            return ticket;
        }

        public List<SupportTicketNotes> GetNotesByTicketId(int id)
        {
            return uow.TicketNoteRepository.Find(n => n.Ticketid == id).ToList();
        }

        public SupportTicketNotes CreateNewNote(int ticketId, string note, int? staffId)
        {
            if (ticketId < 1)
                throw new Exception("Invalid ticket selected!");
            if (string.IsNullOrEmpty(note))
                throw new Exception("Note cannot be empty!");
            var data = new SupportTicketNotes
            {
                DtCreated = DateTime.Now,
                DtModified = DateTime.Now,
                Note = note,
                Ticketid = ticketId,
                Createdbystaffid = staffId,
                Modifiedbystaffid = staffId
            };
            uow.TicketNoteRepository.Add(data);
            uow.Save();
            return data;
        }
    }
}

