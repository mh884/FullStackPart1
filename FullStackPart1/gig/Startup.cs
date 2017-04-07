using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gig.Startup))]
namespace gig
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
