using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class EmployeeNotFoundException:NotFoundException
    {
        public EmployeeNotFoundException(Guid employeId):base($"Zaposleni sa ID-em {employeId} ne postoji u DB!")
        {
            
        }
    }
}
