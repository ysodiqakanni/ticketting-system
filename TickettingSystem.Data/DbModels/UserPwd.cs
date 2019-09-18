using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class UserPwd
    {
        public int Userid { get; set; }
        public string Userpwd1 { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }

        public UserDetails User { get; set; }
    }
}
