using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Repository
{
    public class Model
    {

        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }
        public bool Delete { get; set; }

    }
}
