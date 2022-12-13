using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Dtos
{
    public class BaseResult<T>
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }

        public bool Success { get; set; }

        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
