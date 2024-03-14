using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trainingQuerySide.Abstractions;
using trainingQuerySide.Entities;
using trainingQuerySide.QueryHandlers.GetPendingInvitations;

namespace trainingQuerySide.Infrastructure.Data
{
    public class SubscriptionUserRepository : ISubscriptionUserRepository
    {
        private readonly ApplicationDbContext _context;
        public SubscriptionUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<UserSubscription> FindAsync(string aggregateId, CancellationToken cancellationToken)
        {
            var userSubscription = _context.UserSubscriptions.FirstOrDefault(e=>e.Id == aggregateId);
            if (userSubscription == null) { return null; }
            return Task.FromResult(userSubscription);
        }

        public async Task<GetPendingInvitaionsFilterResult> GetPendingInvitationsAsync(GetPendingInvitationsQuery filter, CancellationToken cancellationToken)
        {
            var query = _context.UserSubscriptions.AsQueryable();

            // give all user subscriptions for the requested user (owner)
            if (!string.IsNullOrWhiteSpace(filter.UserId))
                query = query.Where(t => t.UserId == filter.UserId && t.Status.Equals("Pending"));

            //if (filter.IsCompleted != null)
            //    query = query.Where(t => t.IsCompleted == filter.IsCompleted);

            //if (filter.DueDateFrom != null)
            //    query = query.Where(t => t.DueDate >= filter.DueDateFrom);

            //if (filter.DueDateTo != null)
            //    query = query.Where(t => t.DueDate <= filter.DueDateTo);

            var total = await query.CountAsync(cancellationToken);

            //var results = await query.Skip(filter.Skip)
            //    .Take(filter.Size)
            //    .OrderBy(t => t.ClusterIndex)
            //    .ToListAsync(cancellationToken);
            var results = await query.ToListAsync(cancellationToken);
            return new GetPendingInvitaionsFilterResult(
                Page: filter.Page,
                Size: filter.Size,
                Total: total,
                UserSubscriptions: results
            );

        }



    }
}
