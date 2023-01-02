using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEHR_Interfaces
{
    public interface IBaseResult<T>
    {
         string ResponseMessage { get; set; }
         string ResponseCode { get; set; }

         bool Success { get; set; }

         int StatusCode { get; set; }
         T Data { get; set; }
    }
}
