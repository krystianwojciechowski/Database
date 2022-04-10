using Database.DAO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAO
{
    public abstract class DAO<T>
    {

        public abstract void Insert(params T[] elements);
        public abstract void Update(params T[] elements);
        
        public abstract void Delete( params T[] elements);

        public abstract void Delete(T entity);

        public abstract List<T> Get(T element);

    }
}
