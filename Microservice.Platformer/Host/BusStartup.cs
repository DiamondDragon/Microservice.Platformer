using System.Diagnostics.CodeAnalysis;
using IntelliFlo.Platform.Bus;
using IntelliFlo.Platform.Bus.Serialization;
using JustSaying;
using JustSaying.Messaging.MessageSerialisation;
using Monolith.Bulk.Messaging;

namespace Monolith.Bulk.Host
{
    [ExcludeFromCodeCoverage]
    public class BusConfigurator : DefaultBusConfigurator
    {
        private readonly IBusNamingConvention busNamingConvention;
        private readonly IHandlerResolver handlerResolver;

        public BusConfigurator(IBusNamingConvention busNamingConvention, IHandlerResolver handlerResolver)
        {
            this.busNamingConvention = busNamingConvention;
            this.handlerResolver = handlerResolver;
        }

        public override IMessageSerialisationFactory ConfigureSerialisationFactory()
        {
            return SerializationFactory
                .ForDefault(new NewtonsoftSerialiser())
                .Factory;
        }

        public override void RegisterHandlers(IMayWantOptionalSettings justSaying)
        {
            justSaying
                .WithSqsTopicSubscriber()
                .IntoQueue(busNamingConvention.QueueName())
                .WithMessageHandler<BulkImportUpdated>(handlerResolver)
                .WithSnsMessagePublisher<BulkImportUpdated>();
        }
    }
}
