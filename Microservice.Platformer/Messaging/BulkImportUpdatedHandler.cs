using System.Reflection;
using System.Threading.Tasks;
using IntelliFlo.Platform.Bus;
using IntelliFlo.Platform.Principal.v2;
using log4net;

namespace Microservice.Platformer.Messaging
{
    public class BulkImportUpdatedHandler : IMessageHandlerAsync<BulkImportUpdated>
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IIntelliFloClaimsPrincipal claimsPrincipal;
        private readonly IBusPublisher busPublisher;

        public BulkImportUpdatedHandler(IIntelliFloClaimsPrincipal claimsPrincipal, IBusPublisher busPublisher)
        {
            this.claimsPrincipal = claimsPrincipal;
            this.busPublisher = busPublisher;
        }

        public Task<bool> Handle(IBusContext context, BulkImportUpdated message)
        {
            log.Info($"BulkImport updated. Id: {message.Payload}, UpdatedOn: {message.TimeStamp}");
            log.Info($"context: userid {claimsPrincipal.UserId}, tenant id {claimsPrincipal.TenantId}, subject {claimsPrincipal.Subject}");

            return Task.FromResult(true);
        }
    }
}