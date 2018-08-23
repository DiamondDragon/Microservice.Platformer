using IntelliFlo.Platform.Bus;
using IntelliFlo.Platform.Interceptors;

namespace Microservice.Platformer.Messaging
{
    public class BulkImportUpdated : BusMessage, IEventMessage
    {
        public string Event { get; set; }
        public object Payload { get; set; }
    }
}