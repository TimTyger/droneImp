using AutomataEHR_Services.Interfaces;

namespace AutomataEHR.Response
{
    public class BaseResult<T> :IBaseResult<T>
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }

        public bool Success { get; set; }

        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
