using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Transactions;
using Module = Autofac.Module;

namespace Microservice.Platformer.Modules
{
    public class BulkApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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