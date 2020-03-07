using Domain;
using Domain.Repository;
using Domain.ValueObjects;
using Framework;
using Framework.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class BookStoreAggregate : 
        IStoreAggregate<BookGuid>, IStoreAggregate<BookReaderGuid>
    {
        private IEventRepository _eventRepository;

        public BookStoreAggregate(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<MetaEvent> Save(AggregateRoot<BookGuid> aBook)
        {
            MetaEvent lastEvent = null;
            foreach (IEvent changeCommand in aBook.GetChanges())
            {
                lastEvent = await _eventRepository.Save(changeCommand, aBook);
            }
            aBook.ClearChanges();

            return lastEvent;
        }

        public async Task<MetaEvent> Save(AggregateRoot<BookReaderGuid> aBookReader)
        {
            MetaEvent lastEvent = null;
            foreach (IEvent changeCommand in aBookReader.GetChanges())
            {
                lastEvent = await _eventRepository.Save(changeCommand, aBookReader);
            }
            aBookReader.ClearChanges();

            return lastEvent;
        }
    }
}
