using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
{
    public partial class Departments
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
        public string DeptMgr { get; set; }
    }
}
