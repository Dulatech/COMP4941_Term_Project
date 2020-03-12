using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COMP4941_Term_Project.Startup))]
namespace COMP4941_Term_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
