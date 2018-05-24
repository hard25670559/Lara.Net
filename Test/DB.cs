﻿namespace Test
{
    using Lara.Net.Core.Repository;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DB : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'DB' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'Test.DB' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'DB' 連接字串。
        public DB()
            : base("name=DB")
        {
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<Member> Member { get; set; }

    }

}