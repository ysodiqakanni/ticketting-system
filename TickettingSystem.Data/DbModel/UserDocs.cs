using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class UserDocs
    {
        public int Userid { get; set; }
        public string Idproofdoc { get; set; }
        public string Residencedoc { get; set; }
        public string Profilepic { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }

        public virtual UserDetails User { get; set; }
    }
}
