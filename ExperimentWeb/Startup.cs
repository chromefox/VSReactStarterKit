using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExperimentWeb.Startup))]
namespace ExperimentWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
