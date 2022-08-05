using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Contract.ResponseContract
{
    public class CustomResponseContract
    {
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }

        public static CustomResponseContract Success(object result, HttpStatusCode statusCode)
        {
            return new CustomResponseContract { Result = result, StatusCode = statusCode, Message = "İşlem başarılı", Error = false };
        }

        public static CustomResponseContract Fail(object result, HttpStatusCode statusCode)
        {
            return new CustomResponseContract { Result = result, StatusCode = statusCode, Message = "İşlem başarısız", Error = true };
        }
    }
}
