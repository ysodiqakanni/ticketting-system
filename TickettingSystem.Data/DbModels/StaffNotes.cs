using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class StaffNotes
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public string Note { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
    }
}
