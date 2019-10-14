using System;
using System.Collections.Generic;
using System.Text;

namespace TickettingSystem.Data.DbModel
{
    public partial class SupportTicketNotes
    {
        public int Id { get; set; }
        public int Ticketid { get; set; }
        public string Note { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
        public int? Createdbystaffid { get; set; }
        public int? Modifiedbystaffid { get; set; }
    }
}
