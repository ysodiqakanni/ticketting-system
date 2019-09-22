using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class UserActivityLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActionTypeId { get; set; }
        public DateTime ActionDate { get; set; }
        public string IpAddress { get; set; }
    }
}
