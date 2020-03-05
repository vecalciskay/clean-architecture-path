using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class BookLentEntityTypeConfiguration : IEntityTypeConfiguration<BookLend>
    {
        public void Configure(EntityTypeBuilder<BookLend> builder)
        {
            builder.HasKey(x => x.BookLendId);
            builder.Ignore(x => x.Id);

            builder.Ignore(x => x.BookId);
            builder.Ignore(x => x.BookReaderId);

            //builder.HasOne(x => x.BookRef).WithMany(y => y.HistoryLents).HasForeignKey("BookId");
            //builder.OwnsOne(x => x.BookReaderId)
            //    .Property(y => y.Value)
            //    .HasColumnName("BookReaderId");

            builder.OwnsOne(x => x.DateLent).Property(y => y.Value).HasColumnName("DateLent");
            builder.OwnsOne(x => x.DateReturned).Property(y => y.Value).HasColumnName("DateReturned");
        }
    }
}
