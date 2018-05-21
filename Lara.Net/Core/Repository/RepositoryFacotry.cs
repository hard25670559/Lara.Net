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
            ObjectContainer objectContainer = new ObjectContainer();

            Type repositoryType = typeof(Repository<>);

            Type repositoryAddGeneric = repositoryType.MakeGenericType(new Type[] { RegisterObjectConfig.RepositoryModelContainer[name] });

            IRepository<Model> repository = Activator.CreateInstance(repositoryAddGeneric, null) as IRepository<Model>;
        
            return repository;
        }

    }
}
