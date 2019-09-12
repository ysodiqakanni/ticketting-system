using System;
namespace TickettingSystem.Models
{
    public class ExchangeViewModel
    {
        public ExchangeViewModel()
        {
        }
        public String Name { get; set; }
        public DateTime Enabled { get; set; }
        public String EnteredApi { get; set; }
        public String VerifiedConnection { get; set; }
    }
}
