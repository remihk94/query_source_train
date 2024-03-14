namespace trainingQuerySide.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ISubscriptionUserRepository SubscriptionUsers { get; }
        Task CompleteAsync(CancellationToken cancellationToken);
    }
}
