using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using IntelliFlo.Platform.Transactions;
using Microservice.Platformer.Host.DbInitialization;
using Microservice.Platformer.Services;
using Microservice.Platformer.v2.Mappers;
using Module = Autofac.Module;

namespace Microservice.Platformer.Modules
{
    public class BulkAutofacModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SubSystemTestingInitializer>();
            builder.RegisterType<DevInitializer>();

            builder.RegisterType<BulkImportService>()
                .As<IBulkImportService>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("AutoMapperModule"))
                .As<IModule>()
                .SingleInstance();

            RegisterMappers(builder);
        }

        private void RegisterMappers(ContainerBuilder builder)
        {
            builder.RegisterType<BulkImportMapper>().As<IBulkImportMapper>();
        }
    }
}