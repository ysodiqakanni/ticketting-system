using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class StaffLanguages
    {
        public int Id { get; set; }
        public string Staffuserid { get; set; }
        public int? Languageid { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
    }
}
