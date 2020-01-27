using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AfraMahgoubApp.Startup))]
namespace AfraMahgoubApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
