using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PeopleSearchApplication.Startup))]
namespace PeopleSearchApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
