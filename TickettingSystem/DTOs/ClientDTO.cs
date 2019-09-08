using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.DTOs
{
    public class ClientDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ReferredBy { get; set; }
        public DateTime JoinedDate { get; set; }
        public string KycLevel { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Language { get; set; }
        public string RefUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
