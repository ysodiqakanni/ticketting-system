using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketttingSystem.Lib.entities;

namespace TickettingSystem.Lib.entities
{
    public class User:BaseEntity
    {   [EmailAddress]
        public String Email{get;set;}
        public String Password { get; set; }
        public String UserName { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}