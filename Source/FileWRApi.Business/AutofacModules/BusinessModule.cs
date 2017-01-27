using Autofac;
using FileWR.Business;
using FileWR.Business.Services;

namespace FileWRApi.Business.AutofacModules
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
         //   base.Load(builder);

            builder.RegisterType<FileReader>().As<IFileReader>();
            builder.RegisterType<FileWriter>().As<IFileWriter>();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>();
        }
    }
}
