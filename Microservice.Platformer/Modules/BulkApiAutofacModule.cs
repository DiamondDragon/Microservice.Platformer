using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using IntelliFlo.Platform;
using Module = Autofac.Module;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Transactions;
using Monolith.Bulk.Properties;

namespace Monolith.Bulk.Modules
{
    public class BulkApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new MicroServiceSettings(Settings.Default)).As<IMicroServiceSettings>();

            builder.RegisterAssemblyTypes(this.GetType().Assembly)
                .Where(IsResource)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof (TransactionInterceptor))
                .InstancePerLifetimeScope();
        }

        private static bool IsResource(Type type)
        {
            return typeof(IResource).IsAssignableFrom(type);
        }
    }
}