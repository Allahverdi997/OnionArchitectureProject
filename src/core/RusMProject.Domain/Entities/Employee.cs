using RusMProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProject.Domain.Entities
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        //Relations
        public Department Department { get; set; }
    }
}
