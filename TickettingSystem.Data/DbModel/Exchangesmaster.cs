using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class Exchangesmaster
    {
        public int ExchangeId { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeSignupUrl { get; set; }
        public byte ExchangeValid { get; set; }
        public DateTime ExchangeCrDt { get; set; }
        public DateTime? ExchangeCanDt { get; set; }
        public DateTime? ExchangeModDt { get; set; }
    }
}
