using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using trainingQuerySide.Entities;

namespace trainingQuerySide.Infrastructure.Data.Configurations
{
    public class SubscriptionConfigurations : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.Property(x => x.Sequence).IsConcurrencyToken();
        }
    }
}
