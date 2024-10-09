using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class MaxAgeRangeBadRequestException:BadRequestException
    {
        public MaxAgeRangeBadRequestException():base("Max broj godina ne moze biti manji od Min broj godina!")
        {
            
        }
    }
}
