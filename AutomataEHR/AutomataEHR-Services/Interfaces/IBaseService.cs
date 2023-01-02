using AutomataEHR_Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEHR_Services.Interfaces
{
    public interface IBaseService<TEntity, T>
     where TEntity : class
     where T : BaseDto
        //where TContext : DbContext
    {
        Task<IBaseResult<object>> Add(T dto);
        Task<IBaseResult<object>> AddMultiple(IEnumerable<T> dto);
        Task<IBaseResult<object>> Delete(int id);
        Task<IBaseResult<object>> Get(int id);
        Task<IBaseResult<object>> GetAll();
        Task<IBaseResult<object>> Update(T dto, int id);
        Task<IBaseResult<object>> UpdateMultiple(IEnumerable<T> dto);
    }
}
