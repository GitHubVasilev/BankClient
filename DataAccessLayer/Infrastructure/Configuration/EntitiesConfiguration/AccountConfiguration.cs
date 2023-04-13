using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccessLayer.Infrastructure.Configuration.EntitiesConfiguration
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(m => m.UID);
            builder.Property(m => m.UID).HasColumnType(nameof(Guid)).IsRequired();
            builder.HasKey(m => m.ClientId);
            builder.Property(m => m.ClientId).HasColumnType(nameof(Guid));
            builder.HasOne(m => m.Customer)
                .WithMany(m => m.Accounts)
                .HasForeignKey(m => m.ClientId);

            builder.Property(m => m.Name).HasMaxLength(64);
            builder.Property(m => m.DateOpen).HasColumnType(nameof(Int64)).IsRequired();
            builder.Property(m => m.Procent).HasColumnType(nameof(Double)).IsRequired();
            builder.Property(m => m.CountMonetaryUnit).HasColumnType(nameof(Decimal)).IsRequired();
            builder.Property(m => m.TypeAccount).HasColumnType(nameof(Int32)).IsRequired();
            builder.Property(m => m.IsLock).HasColumnType(nameof(Boolean)).IsRequired();
            builder.Property(m => m.IsClose).HasColumnType(nameof(Boolean)).IsRequired();
        }
    }
}
