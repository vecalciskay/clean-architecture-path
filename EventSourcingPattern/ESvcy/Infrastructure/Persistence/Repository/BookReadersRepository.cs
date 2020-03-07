using Domain.ValueObjects;
using Domain;
using System;
using System.Threading.Tasks;
using Domain.Repository;

namespace Infrastructure.Persistence
{
    public class BookReadersRepository : IBookReadersRepository, IDisposable
    {
        private readonly LibraryDbContext _dbContext;

        public BookReadersRepository(LibraryDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Add(BookReader entity)
        {
            await _dbContext.BookReaders.AddAsync(entity);
        }

        public async Task Delete(BookReaderGuid id)
        {
            BookReader entity = await _dbContext.BookReaders.FindAsync(id);
            _dbContext.BookReaders.Remove(entity);
        }
        
        public async Task<bool> Exists(BookReaderGuid id)
        {
            BookReader b = await _dbContext.BookReaders.FindAsync(id);
            if (b == null)
                return false;
            return true;
        }

        public async Task<BookReader> Load(BookReaderGuid id)
        {
            BookReader aReader = await _dbContext.BookReaders.FindAsync(id.Value);
            aReader.SetId();
            return aReader;
        }

        public void Update(BookReader entity)
        {
            _dbContext.BookReaders.Update(entity);
        }

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
        // ~BooksRepository()
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
