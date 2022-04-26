using Microsoft.EntityFrameworkCore;
using RusMProject.Domain.Entities;
using RusMProject.Persistance.DbContexts;
using RusMProjectApplication.Abstraction.Repositories.ApplicationRepositories.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProject.Persistance.Repositories.ApplicationRepositories.Read
{
    public class EmployeeReadRepository : ReadGenericRepository<Employee>, IEmployeeReadRepository
    {
        public EmployeeReadRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public IQueryable<Employee> GetEmployeesWithInclude()
        {
            return GetAll().Include(x => x.Department);
        }

        public Employee GetEmployeeWithInclude(int id)
        {
            return Table.Where(x => x.Id == id).Include(x => x.Department).FirstOrDefault();
        }
    }
}
