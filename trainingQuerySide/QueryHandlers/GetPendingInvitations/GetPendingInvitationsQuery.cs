using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace trainingQuerySide.QueryHandlers.GetPendingInvitations
{
    public record GetPendingInvitationsQuery(
       int Page,
       int Size,
       // bool? IsCompleted,
       string? UserId
       //DateTime? DueDateFrom,
       //DateTime? DueDateTo
    ) : IRequest<GetPendingInvitaionsFilterResult>
    {
        public int Skip => (Page - 1) * Size;
    }
}
