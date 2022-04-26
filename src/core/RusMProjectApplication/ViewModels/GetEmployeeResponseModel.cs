using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProjectApplication.ViewModels
{
    public class GetEmployeeResponseModel
    {
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
