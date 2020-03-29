namespace COMP4941_Term_Project
{
    public class BranchContext : AppContext
    {
        public BranchContext(string dbInstance) : base(dbInstance)
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}