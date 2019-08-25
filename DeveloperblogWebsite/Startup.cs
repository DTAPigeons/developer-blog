using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeveloperblogWebsite.Startup))]
namespace DeveloperblogWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
