using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
        public string DeptMgr { get; set; }
    }
}
