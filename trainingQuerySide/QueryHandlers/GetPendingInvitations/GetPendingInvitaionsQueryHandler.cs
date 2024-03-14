using MediatR;
using Microsoft.AspNetCore.Mvc;
using trainingQuerySide.Abstractions;
using trainingQuerySide.Infrastructure.Data;

namespace trainingQuerySide.QueryHandlers.GetPendingInvitations
{
    public class GetPendingInvitaionsQueryHandler : IRequestHandler<GetPendingInvitationsQuery, GetPendingInvitaionsFilterResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPendingInvitaionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<GetPendingInvitaionsFilterResult> Handle(GetPendingInvitationsQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.SubscriptionUsers.GetPendingInvitationsAsync(request, cancellationToken);

        }
    }
}
