using Microsoft.AspNetCore.Mvc;
using trainingQuerySide.Entities;
using trainingQuerySide.QueryHandlers.GetPendingInvitations;

namespace trainingQuerySide.Abstractions
{
    public interface ISubscriptionUserRepository
    {
        Task<GetPendingInvitaionsFilterResult> GetPendingInvitationsAsync(GetPendingInvitationsQuery filter, CancellationToken cancellationToken);
        Task<UserSubscription> FindAsync(string aggregateId, CancellationToken cancellationToken);

        //Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        //Task<bool> HasSimilarTodoTaskAsync(string userId, string title, CancellationToken cancellationToken);
        //Task<TodoTask?> GetSimilarTodoTaskAsync(string userId, string title, Guid? excludedId, CancellationToken cancellationToken);
        //Task AddAsync(TodoTask task);
        //Task RemoveAsync(TodoTask task);
    }
}
