using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Api.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime DateEnabled { get; set; }
        public decimal Price { get; set; }
        public int AssignedToStaffId { get; set; }
        public string AssignedToStaffName { get; set; }
    }
    public class TicketConversationDTO
    {
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public bool CreatedByClient { get; set; }  
    }
}
