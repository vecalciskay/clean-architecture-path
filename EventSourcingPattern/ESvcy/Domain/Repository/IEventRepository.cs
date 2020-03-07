using Domain.ValueObjects;
using Framework;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IEventRepository
    {
        Task<MetaEvent> Save(IEvent cmd, AggregateRoot<BookGuid> target);

        Task<MetaEvent> Save(IEvent cmd, AggregateRoot<BookReaderGuid> target);
    }
}
