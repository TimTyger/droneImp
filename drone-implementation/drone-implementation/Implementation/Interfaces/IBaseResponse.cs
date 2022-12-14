using drone_Domain.Dtos;
using System.Net;

namespace drone_implementation.Implementation.Interfaces
{
    public interface IBaseResponse<T>
    {
        Task<BaseResult<T>> Success(object? response, string? respCode = null, string? msg = null);
        Task<BaseResult<T>> CustomErrorMessage(string msg, HttpStatusCode statusCode, string? respCode = null);
        Task<BaseResult<T>> BadRequest(string? msg = null);
        Task<BaseResult<T>> InternalServerError(Exception ex, string? message = null);
        Task<BaseResult<T>> CustomResponse(HttpStatusCode httpStatus, object? response = null, string? respCode = null, string? msg = null);


    }
}
