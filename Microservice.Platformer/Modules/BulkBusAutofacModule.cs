using System;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Module = Autofac.Module;
using IntelliFlo.Platform.Bus;
using IntelliFlo.Platform.Transactions;
using Monolith.Bulk.Host;

namespace Monolith.Bulk.Modules
{
    public class BulkBusAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var typesToRegister = new[] {
                typeof (AwsClientFactory),
                typeof (AwsActiveRegion),
                typeof(BusConfigurator)
            };

            builder.RegisterTypes(typesToRegister)
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterAssemblyTypes(this.GetType().Assembly)
                .Where(IsHandler)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor))
                .InstancePerMatchingLifetimeScope(BusLifetimeTags.ScopePerBusMessageTag);
        }

        private static bool IsHandler(Type type)
        {
            return type.GetInterfaces().Any(x => x.IsGenericType 
                                                 && x.GetGenericTypeDefinition() == typeof(IMessageHandlerAsync<>));
        }
    }
}