using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Utilities
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string Secret { get; set; }
        public int SessionTimeout { get; set; }
    }
}
