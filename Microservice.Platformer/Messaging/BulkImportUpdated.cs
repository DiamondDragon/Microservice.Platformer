using IntelliFlo.Platform.Bus;
using IntelliFlo.Platform.Interceptors;

namespace Monolith.Bulk.Messaging
{
    public class BulkImportUpdated : BusMessage, IEventMessage
    {
        public string Event { get; set; }
        public object Payload { get; set; }
    }
}