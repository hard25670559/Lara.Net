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

    /// <summary>
    /// 對資料庫新增、刪除、修改、讀取的類別！
    /// </summary>
    /// <typeparam name="T">T必須繼承於Model</typeparam>
    public class Repository<T> : IRepository<T> where T : Model
    {

        /// <summary>
        /// 資料庫
        /// </summary>
        protected DbContext DB;

        /// <summary>
        /// 物件管理容器
        /// </summary>
        private ObjectContainer ObjectContainer = new ObjectContainer();

        /// <summary>
        /// Model的名稱
        /// </summary>
        private string ModelName;

        /// <summary>
        /// Model上的欄位集合
        /// </summary>
        private List<string> Collumns = new List<string>();

        /// <summary>
        /// 產生流水號的方式
        /// </summary>
        public ICreateSerialNumber SerialNumber;

        /// <summary>
        /// 初始化Repository
        /// </summary>
        /// <param name="serialNumber">預設使用GUID產生流水號</param>
        public Repository(ICreateSerialNumber serialNumber = null)
        {
            this.SerialNumber = new GUIDSrialNumber();

            if (serialNumber != null)
            {
                this.SerialNumber = serialNumber;
            }

            this.DB = Activator.CreateInstance(RegisterObjectConfig.DBConfig, null) as DbContext;
            this.ObjectContainer.SetObject(this.DB);
            this.ModelName = typeof(T).Name;

            this.SetCollumns();
        }

        /// <summary>
        /// 新增一筆資料至資料庫內！
        /// </summary>
        /// <param name="model">新增的資料</param>
        /// <returns>回傳一個流水號出來</returns>
        public string Create(T model)
        {
            DbSet<T> dbSet = this.ObjectContainer.GetMethod("get_" + this.ModelName) as DbSet<T>;

            //暫時先自動產生GUID，之後要改成使用者可以自行修改流水號的編號方式
            model.SerialNumber = this.SerialNumber.CreateSeriaNumber();
            model.Create = DateTime.Now;
            model.Update = DateTime.Now;
            model.Delete = false;

            dbSet.Add(model as T);
            this.Save();
            return model.SerialNumber;
        }

        /// <summary>
        /// 將Id相符的資料刪除，如果是isSoft為true，則將刪除標記標記成true
        /// ，如果是isSoft為false，則將對應的資料從資料庫中刪除，
        /// isSoft預設為true！
        /// </summary>
        /// <param name="id">資料Id</param>
        /// <param name="isSoft" default="true">是否軟刪除</param>
        /// <returns></returns>
        public virtual bool Delete(int id, bool isSoft = true)
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

        /// <summary>
        /// 抓出對與Id相符的單筆資料
        /// </summary>
        /// <param name="id">資料Id</param>
        /// <returns>傳出對與Id相符的單筆資料</returns>
        public virtual T Read(int id)
        {
            return this.Read().Where(w => w.Id == id).Single();
        }

        /// <summary>
        /// 抓出所有資料表中所有的資料
        /// </summary>
        /// <returns>傳出資料表中所有的資料</returns>
        public virtual List<T> Read()
        {
            DbSet<T> dbSet = this.ObjectContainer.GetMethod("get_" + this.ModelName) as DbSet<T>;

            return (from models in dbSet where !models.Delete select models).ToList();
        }

        /// <summary>
        /// 再DB做完修改之後，需要使用此方法做存檔的動作！
        /// </summary>
        /// <returns>存檔是否成功</returns>
        public bool Save()
        {
            this.DB.SaveChanges();
            return true;
        }

        /// <summary>
        /// 將流水號轉成Id
        /// </summary>
        /// <param name="serialNumber">流水號</param>
        /// <returns>抓出對應的Id</returns>
        public int SerialNumberToId(string serialNumber)
        {
            DbSet<T> dbSet = this.ObjectContainer.GetMethod("get_" + this.ModelName) as DbSet<T>;

            return dbSet.Where(w => w.SerialNumber == serialNumber).Single().Id;
        }

        /// <summary>
        /// 修改資料庫內的資料，如果資料欄位的型態為"數字"型態，
        /// 必須先將資料庫內的資料先蓋上相對的Model，
        /// 不然"數字"型態的資料要是位填寫傳回來會為"0"
        /// </summary>
        /// <param name="id">要修改的那筆資料的Id</param>
        /// <param name="model">更改的資料</param>
        /// <returns>傳回修改成功或失敗</returns>
        public virtual bool Update(int id, T model)
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

        /// <summary>
        /// 透過這個方法抓取資料庫內的所有"非Model"的欄位！
        /// </summary>
        private void SetCollumns()
        {
            List<MethodInfo> tmp = typeof(T).GetMethods().ToList();
            List<MethodInfo> noColl = typeof(Model).GetMethods().ToList();
            foreach (MethodInfo item in tmp)
            {
                bool b = true;
                foreach (var x in noColl)
                {
                    if (x.Name == item.Name)
                        b = false;
                }
                if (b && item.Name.Split('_')[0] == "get")
                    Collumns.Add(item.Name.Split('_')[1]);
            }
        }

    }
}
