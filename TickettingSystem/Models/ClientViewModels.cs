using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Models
{
    public class ClientUpdateViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
    public class ClientListViewModel
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
    public class NoteListViewModel
    {
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