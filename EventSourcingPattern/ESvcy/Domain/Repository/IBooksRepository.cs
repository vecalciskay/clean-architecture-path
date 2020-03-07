using Domain;
using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IBooksRepository
    {
        Task<Book> Load(BookGuid id);
        Task Add(Book entity);
        Task Delete(BookGuid id);
        void Update(Book entity);
        Task<bool> Exists(BookGuid id);
    }
}
