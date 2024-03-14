namespace trainingQuerySide.EventHandlers.MemberOut
{
    public record MemberOutData
    (
        string MemberId,
        string UserId,
        string SubscriptionId,
        string Status
    );
}
