using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class Territories
    {
        public int Id { get; set; }
        public string TerritoryName { get; set; }
        public string TerritoryContinent { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
    }
}
