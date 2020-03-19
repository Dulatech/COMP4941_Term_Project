using COMP4941_Term_Project.Models;
using System.Data.Entity;

namespace COMP4941_Term_Project
{
    public class BranchContext : DbContext
    {
        public BranchContext(string dbInstance) : base(dbInstance)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
    }

    public class BranchDBInitializer : CreateDatabaseIfNotExists<BranchContext>
    {
        protected override void Seed(BranchContext context)
        {
            base.Seed(context);
        }
    }
}