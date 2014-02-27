using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientPoC.Startup))]
namespace ClientPoC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
