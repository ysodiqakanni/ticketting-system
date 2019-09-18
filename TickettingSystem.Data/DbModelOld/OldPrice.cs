using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class OldPrice
    {
        public int PId { get; set; }
        public string Symbol { get; set; }
        public string PSource { get; set; }
        public float AskPrice { get; set; }
        public float BidPrice { get; set; }
        public DateTime Dt { get; set; }
    }
}
