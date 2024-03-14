using trainingQuerySide.Entities;

namespace trainingQuerySide.QueryHandlers.GetPendingInvitations
{
    public record GetPendingInvitaionsFilterResult
    (
        int Page,
        int Size,
        int Total,
        List<UserSubscription> UserSubscriptions
    );
}
