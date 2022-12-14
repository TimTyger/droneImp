using drone_Domain.Dtos;
using drone_Domain.Entities;
using drone_implementation.Implementation.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace drone_implementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity> : ControllerBase
        where TEntity : BaseEntity
    {
        public readonly IBaseService<TEntity, BaseDto> _baseService;
        public readonly IBaseResponse<object> _baseResponse;
        public BaseController(IBaseService<TEntity, BaseDto> baseService, IBaseResponse<object> baseResponse)
        {
            _baseResponse = baseResponse;
            _baseService = baseService;
        }

    }
}
