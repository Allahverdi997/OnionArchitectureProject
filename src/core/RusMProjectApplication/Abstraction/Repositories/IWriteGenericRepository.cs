using RusMProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProjectApplication.Abstraction.Repositories
{
    public interface IWriteGenericRepository<T> : IRepositoryAble<T> where T : class, IEntity
    {
        Task<bool> Add(T model);
        Task AddRange(List<T> models);

        bool Remove(T model);
        void RemoveRange(List<T> models);

        public bool Update(T model);

        void Commit();
    }
}
