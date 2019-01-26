using System.Collections.Generic;

namespace Library.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
    }
}
