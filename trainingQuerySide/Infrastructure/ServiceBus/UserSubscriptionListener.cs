using Azure.Messaging.ServiceBus;
using MediatR;
using System.Text.Json;
using System.Text;
using trainingQuerySide.EventHandlers.MemberOut;

namespace trainingQuerySide.Infrastructure.ServiceBus
{
    public class UserSubscriptionListener : IHostedService
    {
        private readonly ServiceBusSessionProcessor _processor;
        private readonly ILogger<UserSubscriptionListener> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceBusProcessor _deadLetterProcessor;

        public UserSubscriptionListener(UserSubscriptionServiceBus serviceBus, IConfiguration configuration, ILogger<UserSubscriptionListener> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _processor = serviceBus.Client.CreateSessionProcessor(
                topicName: configuration["EmployeesTopic"],
                subscriptionName: configuration["Subscription"],
                options: new ServiceBusSessionProcessorOptions()
                {
                    PrefetchCount = 1,
                    MaxConcurrentSessions = 100,
                    MaxConcurrentCallsPerSession = 1
                });

            _processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
            _processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

            _deadLetterProcessor = serviceBus.Client.CreateProcessor(
                topicName: configuration["UserSubscriptionsTopic"],
                subscriptionName: configuration["Subscription"],
                options: new ServiceBusProcessorOptions()
                {
                    AutoCompleteMessages = false,
                    PrefetchCount = 10,
                    MaxConcurrentCalls = 10,
                    SubQueue = SubQueue.DeadLetter
                });

            //_deadLetterProcessor.ProcessMessageAsync += DeadLetterProcessor_ProcessMessageAsync;
            //_deadLetterProcessor.ProcessErrorAsync += Processor_ProcessErrorAsync;
        }

        //private async Task DeadLetterProcessor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        //{
        //}

        private async Task Processor_ProcessMessageAsync(ProcessSessionMessageEventArgs arg)
        {
            var json = Encoding.UTF8.GetString(arg.Message.Body);

            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var isHandled = arg.Message.Subject switch
            {
                nameof(MemberOut) => await mediator.Send(Deserialize<MemberOut>(json)),
                _ => throw new NotImplementedException(),
                // nameof(EmployeeContactInfoChanged) => await mediator.Send(Deserialize<EmployeeContactInfoChanged>(json)),
                // _ => await mediator.Send(Deserialize<UnknownEvent>(json)),
            };

            if (isHandled)
            {
                await arg.CompleteMessageAsync(arg.Message);
            }
            else
            {
                _logger.LogWarning("Message {MessageId} not handled", arg.Message.MessageId);
                await Task.Delay(5000);
                await arg.AbandonMessageAsync(arg.Message);
            }
        }


        private static T Deserialize<T>(string json)
            => JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? throw new InvalidOperationException("Failed to deserialize message");

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            _logger.LogCritical(arg.Exception, "Message handler encountered an exception," +
                " Error Source:{ErrorSource}," +
                " Entity Path:{EntityPath}",
                arg.ErrorSource.ToString(),
                arg.EntityPath
            );

            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken) => _processor.StartProcessingAsync(cancellationToken);

        public Task StopAsync(CancellationToken cancellationToken) => _processor.CloseAsync(cancellationToken);
    }
}
