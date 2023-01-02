using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEHR_Services.Interfaces
{
    public interface IBaseResponse<T>
    {
        Task<IBaseResult<T>> Success(object? response, string? respCode = null, string? msg = null);
        Task<IBaseResult<T>> CustomErrorMessage(string msg, HttpStatusCode statusCode, string? respCode = null);
        Task<IBaseResult<T>> BadRequest(string? msg = null);
        Task<IBaseResult<T>> InternalServerError(Exception ex, string? message = null);
        Task<IBaseResult<T>> CustomResponse(HttpStatusCode httpStatus, object? response = null, string? respCode = null, string? msg = null);


    }

}
