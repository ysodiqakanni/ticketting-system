using System;
using System.Collections.Generic;
using System.Text;
using static TickettingSystem.Core.Exchange;

namespace TickettingSystem.Core
{
    public class Trade: Entity
    {
        public int UserId { get; set; }
        public string CurrencyCode { get; set; }
        public ExchangeEnum ExchangeCode { get; set; }
        //public DateTime? StartTime { get; set; }
        //public DateTime? EndDate { get; set; }
    }
}
