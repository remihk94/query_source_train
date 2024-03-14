using Grpc.Core;
using MediatR;
using trainingQuerySide;
using trainingQuerySide.Extensions;

namespace trainingQuerySide.Services
{
    public class UserSubscriptionsService : UserSubscriptions.UserSubscriptionsBase
    {
        private readonly ILogger<UserSubscriptionsService> _logger;
        private readonly IMediator _mediator;

        public UserSubscriptionsService(IMediator mediator, ILogger<UserSubscriptionsService> logger)
        {
            _logger = logger;
            _mediator = mediator;

        }

        public override async Task<GetPendingInvitationsResponse> GetPendingInvitations(GetPendingInvitaionsRequest request, ServerCallContext context)
        {
            var query = request.ToQuery();

            var result = await _mediator.Send(query, context.CancellationToken);

          //  var outputs = /*result.UserSubscriptions.Select(t => t.ToFilterOutput())*/;

            return new GetPendingInvitationsResponse()
            {
                
                Page = result.Page,
                PageSize = result.Size
                //Total = result.Total,
                //Invitations =
                //{
                //     //outputs
                //}
            };
        }
    }
}
