using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Repository
{
    public class GUIDSrialNumber : ICreateSerialNumber
    {
        public string CreateSeriaNumber()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
