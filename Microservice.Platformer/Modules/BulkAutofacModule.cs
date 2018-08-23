using System.Reflection;
using Autofac;
using Autofac.Core;
using Monolith.Bulk.Host.DbInitialization;
using Monolith.Bulk.v2.Mappers;
using Module = Autofac.Module;

namespace Monolith.Bulk.Modules
{
    public class BulkAutofacModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SubSystemTestingInitializer>();
            builder.RegisterType<DevInitializer>();

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