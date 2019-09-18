using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class NotificationsUserDetails
    {
        public int NudUserid { get; set; }
        public int? NudNotifymethod { get; set; }
        public string NudSmsnumber { get; set; }
        public string NudTelegramnumber { get; set; }
        public string NudEmail { get; set; }
        public string NudDtCreated { get; set; }
        public string NudDtModified { get; set; }

        public UserDetails NudUser { get; set; }
    }
}
