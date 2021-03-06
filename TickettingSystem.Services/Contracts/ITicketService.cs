﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Services.Contracts
{
    public interface ITicketService
    {
        List<SupportTicket> GetTop10();
        List<SupportTicket> GetParentTicketsByClientNameSuffix(string name);
        List<SupportTicket> GetParentTicketsByClientNamePrefix(string name); 
        List<SupportTicket> GetParentTicketsByClientIdPrefix(string id);
        List<SupportTicket> GetParentTicketsByClientIdSuffix(string id);
        List<SupportTicket> GetParentTicketsByClientName(string name);
        List<SupportTicket> GetParentTicketsByClientId(int id);
        List<SupportTicket> GetConversationsForParentTicket(int parentTicketId);
        SupportTicket CloseTicket(int id);
        SupportTicket Update(int id, int staffId, string note);

        List<SupportTicketNotes> GetNotesByTicketId(int id);
        SupportTicketNotes CreateNewNote(int ticketId, string note, int? staffId);
        SupportTicketNotes UpdateNote(string note, string id, string modifiedBy);
    }
}
