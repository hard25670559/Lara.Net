using Lara.Net.Core.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : Model
    {

        private DbContext DB;
        private ObjectContainer ObjectContainer = new ObjectContainer();
        private string ModelName;
        private List<string> Collumns = new List<string>();

        public Repository()
        {
            this.DB = Activator.CreateInstance(RegisterObjectConfig.DBConfig, null) as DbContext;
            this.ModelName = typeof(T).Name;
            this.ObjectContainer.SetObject(this.DB);
            this.SetCollumns();
        }

        public string Create(T model)
        {
            DbSet<T> dbSet = this.ObjectContainer.GetMethod("get_" + this.ModelName) as DbSet<T>;

            //暫時先自動產生GUID，之後要改成使用者可以自行修改流水號的編號方式
            model.SerialNumber = Guid.NewGuid().ToString("N");
            model.Create = DateTime.Now;
            model.Update = DateTime.Now;
            model.Delete = false;

            dbSet.Add(model as T);
            this.Save();
            return model.SerialNumber;
        }

        public bool Delete(int id, bool isSoft = true)
        {
            DbSet<T> dbSet = this.ObjectContainer.GetMethod("get_" + this.ModelName) as DbSet<T>;

            if (isSoft)
            {
                dbSet.Find(id).Delete = true;
                dbSet.Find(id).Update = DateTime.Now;
            }
            else
                dbSet.Remove(dbSet.Find(id));
            return this.Save();
        }

        public T Read(int id)
        {
            return this.Read().Where(w => w.Id == id).Single();
        }

        public List<T> Read()
        {
            DbSet<T> dbSet = this.ObjectContainer.GetMethod("get_" + this.ModelName) as DbSet<T>;

            return (from models in dbSet where !models.Delete select models).ToList();
        }

        public bool Save()
        {
            this.DB.SaveChanges();
            return true;
        }

        public int SerialNumberToId(string serialNumber)
        {
            DbSet<T> dbSet = this.ObjectContainer.GetMethod("get_" + this.ModelName) as DbSet<T>;

            return dbSet.Where(w => w.SerialNumber == serialNumber).Single().Id;
        }

        public bool Update(int id, T model)
        {
            T saveModel = this.Read(id);
            T tmpModel = model;

            if (saveModel != null && !saveModel.Delete)
            {

                foreach (string item in this.Collumns)
                {
                    MethodInfo setSaveModel = saveModel.GetType().GetMethod("set_" + item);
                    MethodInfo getTmpModel = tmpModel.GetType().GetMethod("get_" + item);

                    object tmpData = getTmpModel.Invoke(tmpModel, null);

                    if (tmpData != null)
                    {
                        setSaveModel.Invoke(saveModel, new object[] { tmpData });
                    }

                }

                saveModel.Update = DateTime.Now;
            }

            return this.Save();
        }

        private void SetCollumns()
        {
            List<MethodInfo> tmp = typeof(T).GetMethods().ToList();
            List<MethodInfo> result = new List<MethodInfo>();

            foreach (MethodInfo item in tmp)
            {

                bool isEquals = item.Name == "Equals";
                bool isGetHashCode = item.Name == "GetHashCode";
                bool isGetType = item.Name == "GetType";
                bool isToString = item.Name == "ToString";

                if (!isEquals && !isGetHashCode && !isGetType && !isToString)
                {
                    bool isId = item.Name.Split('_')[1] == "Id";
                    bool isSerialNumber = item.Name.Split('_')[1] == "SerialNumber";
                    bool isCreate = item.Name.Split('_')[1] == "Create";
                    bool isUpdate = item.Name.Split('_')[1] == "Update";
                    bool isDelete = item.Name.Split('_')[1] == "Delete";

                    if (!isId && !isSerialNumber && !isCreate && !isUpdate && !isDelete)
                    {
                        bool isGet = item.Name.Split('_')[0] == "get";

                        if (!isGet)
                        {
                            this.Collumns.Add(item.Name.Split('_')[1]);
                        }
                    }

                }
            }

        }

    }
}
