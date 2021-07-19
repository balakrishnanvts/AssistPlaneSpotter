using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APS.GenericRepository
{
    public interface IGenericRepository : IDisposable
    {
        Task<IEnumerable<T>> ExecuteList<T>(GenericParameter parameter) where T : class;
        Task<int> Execute(GenericParameter parameter);

    }
}
