using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.BookId);
            builder.Ignore(x => x.Id);
            builder.OwnsOne(x => x.Title)
                .Property(y => y.Value)
                .HasColumnName("Title")
                .HasMaxLength(500);
            builder.OwnsOne(x => x.Version)
                .Property(y => y.Value)
                .HasColumnName("Version");
            //builder.OwnsMany(x => x.HistoryLents);
        }
    }
}
