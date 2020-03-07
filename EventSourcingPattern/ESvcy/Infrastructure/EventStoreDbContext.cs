using Domain;
using Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class EventStoreDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public EventStoreDbContext(
            DbContextOptions<EventStoreDbContext> options,
            ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<MetaEvent> EventStore { get; set; }
        public DbSet<EventSequence> EventSequences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MetaEventEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EventSequenceEntityTypeConfiguration());
        }
    }
}
