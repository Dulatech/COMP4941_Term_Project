using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COMP4941_Term_Project.Models;
using System.Data.Entity;

namespace COMP4941_Term_Project
{
    public class AppContext : DbContext
    {
        public AppContext() : base("ProjectDatabaseV8")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<COMP4941_Term_Project.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<COMP4941_Term_Project.Models.Branch> Branches { get; set; }
    }
    public class AppDBInitializer : CreateDatabaseIfNotExists<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            base.Seed(context);
        }
    }
}