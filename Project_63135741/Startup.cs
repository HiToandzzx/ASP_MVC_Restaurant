using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_63135741.Startup))]
namespace Project_63135741
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
