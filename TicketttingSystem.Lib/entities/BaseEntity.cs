using System;
using System.ComponentModel.DataAnnotations;

namespace TickettingSystem.Lib.entities
{
    public class BaseEntity
    {
        [Key]
        public long Id{get;set;}
        public String CreatedBy{get;set;}
        public String UpdatedBy{get;set;}
        public String CreatedOn{ get; set; }
        public String ModifiedOn{get;set;}
    }
}