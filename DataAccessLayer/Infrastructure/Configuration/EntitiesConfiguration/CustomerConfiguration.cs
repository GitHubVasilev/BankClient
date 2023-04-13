using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccessLayer.Infrastructure.Configuration.EntitiesConfiguration
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(m => m.UID);
            builder.Property(m => m.UID).HasColumnType(nameof(Guid)).IsRequired();

            builder.Property(m => m.FirstName).IsRequired();
            builder.Property(m => m.LastName).IsRequired();
            builder.Property(m => m.Patronymic).IsRequired();
            builder.Property(m => m.Telephone).IsRequired();
            builder.Property(m => m.Passport).IsRequired();
            builder.Property(m => m.DateChange).HasColumnType(nameof(Int64)).IsRequired();
            builder.Property(m => m.FieldChanged).HasColumnType(nameof(Int32)).IsRequired();
            builder.Property(m => m.TypeChanged).HasColumnType(nameof(Int32)).IsRequired();
            builder.Property(m => m.ChangingWorker).HasColumnType(nameof(Int32)).IsRequired();
        }
    }
}
