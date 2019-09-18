using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class UserDetails
    {
        public int Id { get; set; }
        public DateTime Dob { get; set; }
        public int Sex { get; set; }
        public string Userid { get; set; }
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
        public string Referralcode { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
        public int? AccType { get; set; }
        public int? Languageid { get; set; }
        public int? Territoryid { get; set; }

        public NotificationsUserDetails NotificationsUserDetails { get; set; }
        public UserDocs UserDocs { get; set; }
        public UserPwd UserPwd { get; set; }
    }
}
