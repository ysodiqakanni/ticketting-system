using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class StaffDetails
    {
        public int Id { get; set; }
        public DateTime Dob { get; set; }
        public int Sex { get; set; }
        public string Staffuserid { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Emailaddress { get; set; }
        public string Housenumber { get; set; }
        public string Streetname1 { get; set; }
        public string Streetname2 { get; set; }
        public string Streetname3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Countrycode { get; set; }
        public string Phonenumber { get; set; }
        public DateTime? HiredOn { get; set; }
        public DateTime? Firedon { get; set; }
        public DateTime? Resignedon { get; set; }
        public string Hiredbyid { get; set; }
        public int? Departmentid { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
        public string PasswordHash { get; set; }

      
        public string ACCESS_LEVEL { get; set; }
        public string READ_ONLY { get; set; }
    }
}
