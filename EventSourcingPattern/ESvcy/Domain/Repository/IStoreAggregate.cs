using Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IStoreAggregate<TId> where TId : Value<TId>
    {
        Task<MetaEvent> Save(AggregateRoot<TId> obj);
    }
}
