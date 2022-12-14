using drone_Domain.Dtos;

namespace drone_implementation.Implementation.Interfaces
{
    public interface IBaseService<TEntity, T>
     where TEntity : class
     where T : BaseDto
        //where TContext : DbContext
    {
        Task<BaseResult<object>> Add(T dto);
        Task<BaseResult<object>> AddMultiple(IEnumerable<T> dto);
        Task<BaseResult<object>> Delete(int id);
        Task<BaseResult<object>> Get(int id);
        Task<BaseResult<object>> GetAll();
        Task<BaseResult<object>> Update(T dto, int id);
        Task<BaseResult<object>> UpdateMultiple(IEnumerable<T> dto);
    }
}
