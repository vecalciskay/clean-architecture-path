using Framework;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class EFCoreBookUnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _dbContext;

        public EFCoreBookUnitOfWork(LibraryDbContext dbContext)
            => _dbContext = dbContext;

        public Task Commit() => _dbContext.SaveChangesAsync();
    }
}
