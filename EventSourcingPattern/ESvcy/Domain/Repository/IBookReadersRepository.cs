using Domain;
using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IBookReadersRepository
    {
        Task<BookReader> Load(BookReaderGuid id);
        Task Add(BookReader entity);
        Task Delete(BookReaderGuid id);
        void Update(BookReader entity);
        Task<bool> Exists(BookReaderGuid id);
    }
}
