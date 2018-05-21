using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Repository
{
    public interface IUpdate<T> where T : Model
    {

        bool Update(int id, T model);

    }
}
