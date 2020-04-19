using System;
using COMP4941_Term_Project.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace COMP4941_Term_Project
{
    public class AppContext : DbContext
    {
        public AppContext() : base("ProjectDatabaseV1")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public AppContext(string dbInstance) : base(dbInstance)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<FullAddress> FullAddresses { get; set; }

        public DbSet<FullName> FullNames { get; set; }


        //////////////////////////////////
        ///
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return 0;
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            Console.WriteLine(exceptionMessage);
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }

    }
    public class AppDBInitializer : CreateDatabaseIfNotExists<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            base.Seed(context);
        }
    }

}