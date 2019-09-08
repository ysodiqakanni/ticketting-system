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
}
