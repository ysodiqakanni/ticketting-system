using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class Notifications
    {
        public int Notificationid { get; set; }
        public int NUserid { get; set; }
        public int? NCurrPair { get; set; }
        public int? NCondition { get; set; }
        public double NConditionValue { get; set; }
        public DateTime NDtRequested { get; set; }
        public DateTime? NDtExecuted { get; set; }
        public DateTime? NDtCancelled { get; set; }
        public int? NStatus { get; set; }
    }
}
