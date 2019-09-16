using System;
using System.Collections.Generic;
using System.Text;

namespace TickettingSystem.Core
{
    public class Exchange: Entity
    {
        public string ExchangeName { get; set; }
        public DateTime DateEnabled { get; set; }
        public string APIsEntered { get; set; }
        public int ExchangeUserId { get; set; }
    }
}
