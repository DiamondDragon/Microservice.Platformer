using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using IntelliFlo.Platform;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Transactions;
using Microservice.Platformer.Properties;
using Module = Autofac.Module;

namespace Microservice.Platformer.Modules
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