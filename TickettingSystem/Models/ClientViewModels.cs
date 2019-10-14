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
       
        [Display(Name ="House Number")]
        [Required, MaxLength(10)]
        public string HouseNumber { get; set; }

        [Display(Name = "Street name 1")]
        [Required, MaxLength(45)]
        public string StreetName1 { get; set; }
        [MaxLength(45)]

        [Display(Name = "Street name 2")]
        public string StreetName2 { get; set; }

        [Display(Name = "Street name 3")]
        [MaxLength(45)]
        public string StreetName3 { get; set; }

        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Language { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage ="Date of birth is required")]
        public DateTime Dob { get; set; }
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
        public int ID { get; set; }
        public string Note { get; set; }
        public string ShortNote
        {
            get
            {
                if (Note.Length > 50)
                {
                    return Note.Substring(0, 50) + "...";
                }
                return Note;
            }
        }
    }
}