using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Repository
{
    public interface IRead<T> where T : Model
    {

        T Read(int id);

        List<T> Read();

    }
}
