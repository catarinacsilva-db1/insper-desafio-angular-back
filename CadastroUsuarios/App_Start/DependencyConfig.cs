using Autofac;
using Autofac.Integration.WebApi;
using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Data;
using CadastroUsuarios.Repositories;
using CadastroUsuarios.Service;
using CadastroUsuarios.Service.Utils;
using System.Reflection;
using System.Web.Http;

namespace CadastroUsuarios.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppDbContext>().InstancePerRequest();

            builder.RegisterType<UsuarioRepository>().As<IUsuarioRepository>().InstancePerRequest();
            builder.RegisterType<UsuarioService>().As<IUsuarioService>().InstancePerRequest();

            builder.RegisterType<Validators>().InstancePerRequest();
            builder.RegisterType<FiltroQueries>().InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}