using Database.DAO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAO
{
    public abstract class DAO<T,F>
    {

        public abstract void Insert(params T[] elements);
        public abstract void Update(params T[] elements);
        
        public abstract void Delete( params T[] elements);

        public abstract T Get(Filter<F> filter, T element);

    }
}
