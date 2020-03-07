using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class EventSequenceEntityTypeConfiguration : IEntityTypeConfiguration<EventSequence>
    {
        public void Configure(EntityTypeBuilder<EventSequence> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
