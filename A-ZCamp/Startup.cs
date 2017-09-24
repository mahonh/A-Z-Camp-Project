using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(A_ZCamp.Startup))]
namespace A_ZCamp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
