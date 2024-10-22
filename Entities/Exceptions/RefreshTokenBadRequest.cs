using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RefreshTokenBadRequest:BadRequestException
    {
        public RefreshTokenBadRequest():base("Nevalidan zahtjev klijenta. TokenDto ima neke nevalidne vrijednosti.")
        {
            
        }
    }
}
