using System.Threading.Tasks;
using IntelliFlo.Platform.Bus;
using IntelliFlo.Platform.Principal.v2;
using Microsoft.Extensions.Logging;

namespace Microservice.Platformer.Messaging
{
    public class BulkImportUpdatedHandler : IMessageHandlerAsync<BulkImportUpdated>
    {
        private readonly ILogger<BulkImportUpdatedHandler> logger;
        private readonly IIntelliFloClaimsPrincipal claimsPrincipal;
        private readonly IBusPublisher busPublisher;

        public BulkImportUpdatedHandler(ILogger<BulkImportUpdatedHandler> logger, IIntelliFloClaimsPrincipal claimsPrincipal, IBusPublisher busPublisher)
        {
            this.logger = logger;
            this.claimsPrincipal = claimsPrincipal;
            this.busPublisher = busPublisher;
        }

        public Task<bool> Handle(IBusContext context, BulkImportUpdated message)
        {
            logger.LogInformation("BulkImport updated. Id: {payload}, UpdatedOn: {timeStamp}", message.Payload, message.TimeStamp);
            logger.LogInformation("context: userid {userId}, tenant id {tenantId}, subject {subject}", claimsPrincipal.UserId, claimsPrincipal.TenantId, claimsPrincipal.Subject);

            return Task.FromResult(true);
        }
    }
}