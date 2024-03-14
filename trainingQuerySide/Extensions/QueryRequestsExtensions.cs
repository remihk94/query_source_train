using trainingQuerySide.QueryHandlers.GetPendingInvitations;

namespace trainingQuerySide.Extensions
{
    public static class QueryRequestsExtensions
    {
        public static GetPendingInvitationsQuery ToQuery(this GetPendingInvitaionsRequest request)
          => new(
              Page: request.Page ?? 1,
              Size: request.PageSize ?? 25,
              UserId: request.UserId
              
          );

      
    }
}
