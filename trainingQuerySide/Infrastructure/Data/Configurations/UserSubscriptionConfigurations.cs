using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using trainingQuerySide.Entities;

namespace trainingQuerySide.Infrastructure.Data.Configurations
{
    public class UserSubscriptionConfigurations : IEntityTypeConfiguration<UserSubscription>
    {
        
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.Property(x => x.Sequence).IsConcurrencyToken();
        }
    }
}
