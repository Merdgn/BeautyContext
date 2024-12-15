using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Entity.Concrete
{
    public class EmployeeService
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}

