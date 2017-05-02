using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service.Model
{
    public class AADModel
    {
        public string AAD_Tenant { get; set; }

        public string AAD_Audience { get; set; }

        public string AAD_ClientId { get; set; }

        public string AAD_AuthUri { get; set; }

        public string AAD_Key { get; set; }

        public string AAD_Resource { get; set; }
    }
}
