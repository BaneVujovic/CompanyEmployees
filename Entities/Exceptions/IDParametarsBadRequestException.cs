using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class IDParametarsBadRequestException:BadRequestException
    {
        public IDParametarsBadRequestException() : base("ID parametar je null!") { }
    }
}
