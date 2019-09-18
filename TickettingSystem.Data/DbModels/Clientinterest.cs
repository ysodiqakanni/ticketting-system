using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class Clientinterest
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Emailaddress { get; set; }
        public int? Housenumber { get; set; }
        public string Streetname1 { get; set; }
        public string Streetname2 { get; set; }
        public string Streetname3 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phonenumber { get; set; }
    }
}
