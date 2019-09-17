
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Models
{
    public class DashboardViewModel
    {
        public List<ClientListViewModel> Clients { get; set; }
        public List<TradeViewModel> Trades { get; set; }

        public List<NoteListViewModel> Notes { get; set; }
        public List<ExchangeListViewModel> Exchanges { get; set; }

        public Membership MembershipData { get; set; }
        public List<StaffListViewModel> StaffList { get; set; }
        public List<StaffNoteViewModel> NotesForTheSelectedStaff { get; set; }
        public List<TicketsListViewModel> Tickets { get; set; }
        public List<TicketConversationViewModel> TicketConversations { get; set; }
        public List<NoteListViewModel> NotesForSelectedTicketClient { get; set; }
    }
}