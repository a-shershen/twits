using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.DAL.Interfaces
{
    public interface IGenericRepo<T>
    {
        void Create(T item);

        T Read(Func<T, bool> predicate);

        void Update(T item);

        void Delete(Func<T, bool> predicate);

        IEnumerable<T> GetAll(Func<T, bool> predicate = null);

        IQueryable<T> GetAllQueryable(Func<T, bool> predicate = null);
    }
}
