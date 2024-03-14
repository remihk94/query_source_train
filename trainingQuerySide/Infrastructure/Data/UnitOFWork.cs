using trainingQuerySide.Abstractions;

namespace trainingQuerySide.Infrastructure.Data
{
    public class UnitOFWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;

        public ISubscriptionUserRepository SubscriptionUsers { get; } = new SubscriptionUserRepository(context);

        public Task CompleteAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

        public void Dispose() => _context.Dispose();
    }
}
