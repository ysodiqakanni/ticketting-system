using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class CurrentPrice
    {
        public int CpId { get; set; }
        public string CSymbol { get; set; }
        public string CpSource { get; set; }
        public float CAskPrice { get; set; }
        public float CBidPrice { get; set; }
        public DateTime CDt { get; set; }
    }
}
