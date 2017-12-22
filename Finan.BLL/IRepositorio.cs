using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.BLL
{
    public interface IRepositorio<T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(T model);
        void Delete(params object[] keyValues);
        T Get(params object[] keyValues);
        ICollection<T> GetWhere(Func<T, bool> predicate);
        ICollection<T> GetAll();
    }
}
