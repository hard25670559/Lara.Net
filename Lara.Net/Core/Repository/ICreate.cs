using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Repository
{
    public interface ICreate<T> where T : Model
    {

        string Create(T model);

    }
}
