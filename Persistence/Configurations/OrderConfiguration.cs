using Domain.Customers;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasConversion(
                order => order.Value,
                value => new OrderId(value));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasOne(o => o.AppUser)
                .WithMany()
                .HasForeignKey(o => o.AppUserId);

            builder.HasMany(o => o.LineItems)
                .WithOne(li => li.Order)
                .HasForeignKey(li => li.OrderId);
        }
    }
}
