using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class StaffTerritory
    {
        public int Id { get; set; }
        public string Staffuserid { get; set; }
        public int? Territory { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
    }
}
