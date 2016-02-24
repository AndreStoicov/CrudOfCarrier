using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudOfCarrier.Startup))]
namespace CrudOfCarrier
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
