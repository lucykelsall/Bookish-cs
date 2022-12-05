using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookish_cs.Web.Startup))]
namespace Bookish_cs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
