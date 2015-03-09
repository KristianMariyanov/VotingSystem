using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VotingSystem.Web.Startup))]
namespace VotingSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
