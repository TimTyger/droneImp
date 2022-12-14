using drone_Domain.Dtos;
using drone_implementation.Implementation.Interfaces;
using System.Net;

namespace drone_implementation.Implementation.Services
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public BaseResponse()
        {
        }

        public Task<BaseResult<T>> Success(object response = null, string respCode = null, string msg = null)
        {
            try
            {
                var obj = new BaseResult<T>
                {
                    ResponseMessage = msg == null ? "Successful" : msg,
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    ResponseCode = respCode == null ? "00" : respCode,
                    Data = (T)response
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResult<T>());
            }
        }
        public Task<BaseResult<T>> CustomResponse(HttpStatusCode httpStatus, object response = null, string respCode = null, string msg = null)
        {
            try
            {
                var obj = new BaseResult<T>
                {
                    ResponseMessage = msg == null ? "Successful" : msg,
                    Success = true,
                    StatusCode = (int)(httpStatus),
                    ResponseCode = respCode == null ? "00" : respCode,
                    Data = (T)response
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResult<T>());
            }
        }

        public Task<BaseResult<T>> SuccessMessage(string msg)
        {
            try
            {
                var obj = new BaseResult<T>
                {
                    ResponseMessage = msg,
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    ResponseCode = "00"
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResult<T>());
            }
        }

        public Task<BaseResult<T>> CustomErrorMessage(string msg, HttpStatusCode statusCode, string respCode = null)
        {
            try
            {
                var obj = new BaseResult<T>
                {
                    ResponseMessage = msg,
                    Success = false,
                    StatusCode = (int)statusCode,
                    ResponseCode = "99"
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResult<T>());
            }
        }

        public Task<BaseResult<T>> BadRequest(string msg = null)
        {
            try
            {
                var obj = new BaseResult<T>
                {
                    ResponseCode = "99",
                    ResponseMessage = msg,
                    Success = false,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResult<T>());
            }
        }

        public Task<BaseResult<T>> InternalServerError(Exception ex, string message = null)
        {
            try
            {
                var obj = new BaseResult<T>
                {
                    ResponseCode = "99",
                    ResponseMessage = message == null ? ex.Message : message,
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return Task.FromResult(obj);
            }
            catch (Exception exp)
            {

                return Task.FromResult(new BaseResult<T>());
            }
        }


    }

}
