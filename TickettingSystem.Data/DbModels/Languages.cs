using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class Languages
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
    }
}
