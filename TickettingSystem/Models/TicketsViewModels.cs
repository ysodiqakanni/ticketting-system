using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Models
{
    public class TicketsListViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime DateEnabled { get; set; }
        public decimal Price { get; set; }
        public int AssignedToStaffId { get; set; }
    }
    public class TicketConversationViewModel
    {
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public bool CreatedByClient { get; set; }   // A Conversation message is either sent by a client or a staff
    }
}