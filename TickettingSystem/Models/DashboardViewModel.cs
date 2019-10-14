
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.DTOs;

namespace TickettingSystem.Models
{
    public class DashboardViewModel
    {
        public List<ClientDTO> Clients { get; set; }
        public List<TradeDTO> Trades { get; set; }

        public List<NoteListViewModel> NotesForSelectedClient { get; set; }
        public List<ExchangeListViewModel> Exchanges { get; set; }

        public Membership MembershipData { get; set; }
        public List<StaffListViewModel> StaffList { get; set; }
        public List<StaffNoteViewModel> NotesForTheSelectedStaff { get; set; }
        public List<TicketsListViewModel> Tickets { get; set; }
        public List<TicketConversationViewModel> TicketConversations { get; set; }
        public List<NoteListViewModel> NotesForSelectedTicketClient { get; set; }

        public List<LanguageViewModel> Languages { get; set; }
        public List<TeritoryViewModel> Teritories { get; set; }
    }
    public class LanguageViewModel
    {
        public int Id { get; set; }
        public string Language { get; set; }
    }
    public class TeritoryViewModel
    {
        public int Id { get; set; } 
        public string TerritoryName { get; set; }
        public string TerritoryContinent { get; set; } 
    }
}