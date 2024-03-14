using Azure.Messaging.ServiceBus;

namespace trainingQuerySide.Infrastructure.ServiceBus
{
    public class UserSubscriptionServiceBus(IConfiguration configuration)
    {
        public ServiceBusClient Client { get; } = new ServiceBusClient(configuration["UserSubscriptionServiceBusConnectionString"]);

    }
}
