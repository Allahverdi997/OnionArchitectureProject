using RusMProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProject.Domain.Entities
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }

        //Relations
        public List<Employee> Employees { get; set; }
    }
}
