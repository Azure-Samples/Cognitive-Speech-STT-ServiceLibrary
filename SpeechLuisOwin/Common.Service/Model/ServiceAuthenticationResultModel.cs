using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service.Model
{
    public class ServiceAuthenticationResultModel 
    {
        public string AccessToken { get; set; }

        public DateTimeOffset ExpiresOn { get; set; }
    }
}
