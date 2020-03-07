using Microsoft.Extensions.Logging;
using Domain;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.EntityConfiguration;

namespace Infrastructure.Persistence
{
    public class LibraryDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public LibraryDbContext(
            DbContextOptions<LibraryDbContext> options,
            ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookReader> BookReaders { get; set; }
        public DbSet<BookLend> BookLends { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookReaderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookLentEntityTypeConfiguration());
        }
    }
}
