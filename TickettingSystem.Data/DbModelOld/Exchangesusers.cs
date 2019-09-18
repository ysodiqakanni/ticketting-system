using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class Exchangesusers
    {
        public int EuId { get; set; }
        public int ExchangeuId { get; set; }
        public int Exchangeuuserid { get; set; }
        public string Exchangeuapi1 { get; set; }
        public string Exchangeuapi2 { get; set; }
        public int Exchangeunonce { get; set; }
        public DateTime ExchangeuCrDt { get; set; }
        public DateTime? ExchangeuCanDt { get; set; }
        public DateTime? ExchangeuModDt { get; set; }
        public int? ExchangeuStatus { get; set; }
        public string ExchangeuUserid1 { get; set; }
    }
}
