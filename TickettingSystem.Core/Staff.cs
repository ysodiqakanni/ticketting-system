using System;
using System.Collections.Generic;
using System.Text;

namespace TickettingSystem.Core
{
    public class Staff: Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName1 { get; set; }
        public string StreetName2 { get; set; }
        public string StreetName3 { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ReferredBy { get; set; }
        public DateTime HiredOn { get; set; }
        public DateTime FiredOn { get; set; }
        public DateTime ResignedOn { get; set; }
        public string HiredBy { get; set; }
    }
}
