using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BigBoss.Startup))]
namespace BigBoss
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
