using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class SupportTicket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentTypeId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime? DtCreated { get; set; }
        public DateTime? DtUpdated { get; set; }
        public int? AssignedTo { get; set; }
        public int? ParentTicketId { get; set; }
        public DateTime? DtClosed { get; set; }
    }
}
