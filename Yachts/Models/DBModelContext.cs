using System;
using System.Data.Entity;
using System.Linq;

namespace Yachts.Models
{
    public class DBModelContext : DbContext
    {
        // Your context has been configured to use a 'DBModelContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Yachts.Models.DBModelContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBModelContext' 
        // connection string in the application configuration file.
        public DBModelContext()
            : base("name=DBModelContext")
        {
        }

        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<NewsAttachment> NewsAttachments { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}