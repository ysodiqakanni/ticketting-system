using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Models
{
    public class DashboardViewModel
    {
        public List<ClientListViewModel> Clients { get; set; }
        public List<TradeViewModel> Trades { get; set; }
    }
}
