using System;
using System.Collections.Generic;
using TickettingSystem.Lib.entities;

namespace TicketttingSystem.Lib.entities
{
    public class Role:BaseEntity
    {
        public Role()
        {
        }
        public String Name { get; set; }
        public String Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
