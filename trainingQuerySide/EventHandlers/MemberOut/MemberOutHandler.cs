using MediatR;
using trainingQuerySide.Abstractions;
using trainingQuerySide.Infrastructure.Data;

namespace trainingQuerySide.EventHandlers.MemberOut
{
    public class MemberOutHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<MemberOutHandler> logger) : IRequestHandler<MemberOut, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<MemberOutHandler> _logger = logger;
       

        public async Task<bool> Handle(MemberOut @event, CancellationToken cancellationToken)
        {
            var usersubscription =  await _unitOfWork.SubscriptionUsers.FindAsync(@event.AggregateId, cancellationToken);
            if (usersubscription is null)
            {
                _logger.LogWarning("user-subscription {usersubscription} not found", @event.AggregateId);
                return false;
            }

           // var subscription = await _unitOfWork.Subscriptions.FindAsync(@event.Data.SubscriptionId, cancellationToken);
          //  if(!subscription.UserSubscriptions.Any(t => t.MemberId == @event.Data.MemberId ))
            // i dont know if these condition is in the right place
            if (!@event.Data.Status.Equals("Accepted"))
            {
                _logger.LogWarning("member {UserId} can not leave subscription {SubscriptionId} that he is not joined to it", @event.Data.UserId, @event.Data.SubscriptionId);
                return false;
            }
            if(@event.Data.MemberId.Equals(@event.Data.UserId))
            {
                _logger.LogWarning("member can not leave subscription that he ownes");
                return false;
            }
            if (@event.Sequence <= usersubscription.Sequence) return true;

            if (@event.Sequence > usersubscription.Sequence + 1)
            {
                _logger.LogWarning("Sequence {Sequence} is not expected for user-subscription {UserSubscriptionId}", @event.Sequence, @event.AggregateId);
                return false;
            }

            usersubscription.MemberOut(@event);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return true;
        }
    }
}
