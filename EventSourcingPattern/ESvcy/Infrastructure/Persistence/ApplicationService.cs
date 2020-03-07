using Domain;
using Domain.Repository;
using Domain.ValueObjects;
using Framework;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
public abstract class ApplicationService : IApplicationService
{
    protected IStoreAggregate<BookGuid> _bookstoreAggregate;
    protected IStoreAggregate<BookReaderGuid> _readerstoreAggregate;

    public abstract Task Handle(object command);

    public async Task<MetaEvent> SaveAggregateInEventStore(Book aBook)
        => await _bookstoreAggregate.Save(aBook);

    public async Task<MetaEvent> SaveAggregateInEventStore(BookReader aReader)
        => await _readerstoreAggregate.Save(aReader);
          
}
}
