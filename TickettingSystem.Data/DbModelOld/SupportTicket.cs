﻿using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
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
    }
}
