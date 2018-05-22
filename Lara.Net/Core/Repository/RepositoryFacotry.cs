using Lara.Net.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Repository
{
    public class RepositoryFacotry
    {

        public static IRepository<Model> Create(string name)
        {

            IRepository<Model> repository = Activator.CreateInstance(RegisterObjectConfig.RepositoryModelContainer[name], null) as IRepository<Model>;

            return repository;

        }

    }
}
