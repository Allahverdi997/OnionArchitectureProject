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
    public class DepartmentReadRepository:ReadGenericRepository<Department>,IDepartmentReadRepository
    {
        public DepartmentReadRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }
    }
}
