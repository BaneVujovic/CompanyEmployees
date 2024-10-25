using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public abstract class ApiNotFoundRespose : ApiBaseResponse {
        public string Message { get; set; }
        public ApiNotFoundRespose(string message):base(false)
        {
            Message = message;
        }

    }
}
