using CoffeeAndSnackVendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeAndSnackVendingMachine.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName)
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .HasMaxLength(50);
        }
    }
}
