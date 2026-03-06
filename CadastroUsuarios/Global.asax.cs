using CadastroUsuarios.App_Start;
using System.Web.Http;

namespace CadastroUsuarios
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DependencyConfig.RegisterDependencies();
        }
    }
}
