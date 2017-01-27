using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using FileWRApi.Business.AutofacModules;

namespace FileWRApi.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Register individual components
            // builder.RegisterType<class>().As<Interface>();

            builder.RegisterModule(new BusinessModule());
            
            // Scan an assembly for components
          //  builder.RegisterAssemblyTypes(Assembly.GetCallingAssembly());
                
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}