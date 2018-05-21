using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core
{
    public class ObjectContainer
    {

        private object ReflectionOfObject = null;
        private Type ReflectionOfType = null;

        public object GetObject(string fullClassName, params object[] construct)
        {
            Type type = Type.GetType(fullClassName);
            this.ReflectionOfType = type;

            object reflectionObject = Activator.CreateInstance(type, construct);

            this.ReflectionOfObject = reflectionObject;

            return reflectionObject;
        }

        public void SetObject(object instance)
        {
            this.ReflectionOfType = instance.GetType();
            this.ReflectionOfObject = instance;
        }

        public object GetMethod(string methodName, params object[] parameter)
        {

            List<MethodInfo> methodInfos = this.ReflectionOfType.GetMethods().ToList();

            MethodInfo methodInfo = this.ReflectionOfType.GetMethod(methodName);

            object result = methodInfo.Invoke(this.ReflectionOfObject, parameter);

            return result;
        }


        public class Test
        {
            public string CCC()
            {
                return "owifjef";
            }
        }


    }
}
