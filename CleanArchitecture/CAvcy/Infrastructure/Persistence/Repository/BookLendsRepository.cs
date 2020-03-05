using Domain.ValueObjects;
using Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Repository;

namespace Infrastructure.Persistence
{
    public class BookLendsRepository : IBookLendsRepository, IDisposable
    {
        private readonly LibraryDbContext _dbContext;

        public BookLendsRepository(LibraryDbContext dbContext)
            => _dbContext = dbContext;

        public List<BookLend> LoadLends(BookGuid id)
        {
            List<BookLend> result = 
                _dbContext.BookLends.Where(t => t.BookRef.BookId == id.Value).ToList(); 
            return result;
        }

        public async Task<BookLend> LoadLend(Guid id) 
            => await _dbContext.BookLends.FindAsync(id);

        public Task Delete(BookLend obj)
        {
            _dbContext.BookLends.Remove(obj);
            return Task.CompletedTask;
        }

        public async Task Add(BookLend newLend) 
            => await _dbContext.BookLends.AddAsync(newLend);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BookLendsRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
