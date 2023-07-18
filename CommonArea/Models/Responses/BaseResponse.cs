using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Models.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }


        public BaseResponse(bool _success, string _msg, T _data)
        {
            this.Success = _success;
            this.Data = _data;
            this.Message = _msg;
        }

    }



}
