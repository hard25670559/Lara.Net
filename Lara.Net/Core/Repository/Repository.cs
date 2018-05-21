using Lara.Net.Core.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Model
    {

        private DbContext DB;
        private ObjectContainer ObjectContainer = new ObjectContainer();
        private string ModelName;

        public Repository(DbContext db)
        {
            this.DB = db;
            this.ModelName = typeof(T).Name;
            this.ObjectContainer.SetObject(this.DB);
        }

        public bool Create(T model)
        {
            DbSet<T> dbSet = this.ObjectContainer.GetMethod("get_" + this.ModelName) as DbSet<T>;

            model.SerialNumber = "";
            model.Create = DateTime.Now;
            model.Update = DateTime.Now;
            model.Delete = false;

            dbSet.Add(model as T);
            return this.Save();
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

        public abstract bool Update(int id, T model);
    }
}
