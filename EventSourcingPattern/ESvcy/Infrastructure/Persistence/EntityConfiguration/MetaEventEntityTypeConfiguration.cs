using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class MetaEventEntityTypeConfiguration : IEntityTypeConfiguration<MetaEvent>
    {
        public void Configure(EntityTypeBuilder<MetaEvent> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
