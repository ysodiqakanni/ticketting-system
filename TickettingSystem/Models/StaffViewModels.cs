using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Models
{
    public class StaffListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ReferredBy { get; set; }
        public DateTime JoinedOn { get; set; }
        public string KycLevel { get; set; }
    }

    public class StaffNoteViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public string ShortNote
        {
            get
            {
                if (Content.Length > 50)
                {
                    return Content.Substring(0, 50) + "...";
                }
                return Content;
            }
        }
    }
}
