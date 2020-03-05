using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class BookReaderEntityTypeConfiguration : IEntityTypeConfiguration<BookReader>
    {
        public void Configure(EntityTypeBuilder<BookReader> builder)
        {
            builder.HasKey(x => x.BookReaderId);
            builder.Ignore(x => x.Id);
            builder.OwnsOne(x => x.Name).Property(y => y.Value).HasMaxLength(200);
        }
    }
}
