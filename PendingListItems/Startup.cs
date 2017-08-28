using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PendingListItems.Startup))]
namespace PendingListItems
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
