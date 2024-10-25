using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ApiCompanyNotFoundException : ApiNotFoundRespose
    {
        public ApiCompanyNotFoundException(Guid companyId) : base($"Kompanija sa ID-em {companyId} ne postoji u DB.")
        {

        }
    }
}
