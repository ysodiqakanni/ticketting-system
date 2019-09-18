using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class Socialtradingproviders
    {
        public int TraderUid { get; set; }
        public string Strategytitle { get; set; }
        public string Strategydescription { get; set; }
        public string Strategyactive { get; set; }
        public DateTime? Strategystartedon { get; set; }
        public DateTime? Strategymodifiedon { get; set; }
        public DateTime? Strategycancelledon { get; set; }
    }
}
