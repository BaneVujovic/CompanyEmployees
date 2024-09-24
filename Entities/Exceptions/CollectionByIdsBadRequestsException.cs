using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CollectionByIdsBadRequestsException:BadRequestException
    {
        public CollectionByIdsBadRequestsException() : base("Broj kolekcije se ne poklapa sa broem ID-eva!") { }
    }
}
