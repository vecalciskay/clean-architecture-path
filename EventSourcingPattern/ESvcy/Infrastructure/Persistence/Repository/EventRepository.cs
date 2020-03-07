using Domain;
using Domain.Repository;
using Domain.ValueObjects;
using Framework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class EventRepository : IEventRepository, IDisposable
    {
        private readonly EventStoreDbContext _storeDbContext;

        public EventRepository(EventStoreDbContext storeContext)
        {
            _storeDbContext = storeContext;
        }

        public async Task<MetaEvent> Save(IEvent @event, AggregateRoot<BookGuid> target)
        {            
            JsonSerializer json = JsonSerializer.CreateDefault();
            StringBuilder serialized = new StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(serialized);
            json.Serialize(writer, @event);

            EventSequence sequence = await GetOrCreateSequence();

            MetaEvent eventToSave = new MetaEvent() {
                Id = Guid.NewGuid(),
                StreamName = "BOOK",
                EventClass = @event.GetType().FullName,
                InstanceId = target.Id,
                SerializedEvent = writer.ToString(),
                Version = sequence.NextSequence
            };

            sequence.NextSequence++;
            _storeDbContext.EventSequences.Update(sequence);

            await _storeDbContext.EventStore.AddAsync(eventToSave);
            await _storeDbContext.SaveChangesAsync();

            return eventToSave;
        }
        public async Task<MetaEvent> Save(IEvent @event, AggregateRoot<BookReaderGuid> target)
        {
            JsonSerializer json = JsonSerializer.CreateDefault();
            StringBuilder serialized = new StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(serialized);
            json.Serialize(writer, @event);

            EventSequence sequence = await GetOrCreateSequence();

            MetaEvent eventToSave = new MetaEvent()
            {
                Id = Guid.NewGuid(),
                StreamName = "BOOKREADER",
                EventClass = @event.GetType().FullName,
                InstanceId = target.Id,
                SerializedEvent = writer.ToString(),
                Version = sequence.NextSequence
            };

            sequence.NextSequence++;
            _storeDbContext.EventSequences.Update(sequence);

            await _storeDbContext.EventStore.AddAsync(eventToSave);
            await _storeDbContext.SaveChangesAsync();

            return eventToSave;
        }

        private async Task<EventSequence> GetOrCreateSequence()
        {
            List<EventSequence> sequences = await _storeDbContext.EventSequences.ToListAsync();
            if (sequences.Count == 0)
            {
                sequences = new List<EventSequence>();
                sequences.Add(new EventSequence()
                {
                    Id = Guid.NewGuid(),
                    NextSequence = 1
                });
                await _storeDbContext.EventSequences.AddAsync(sequences[0]);
                await _storeDbContext.SaveChangesAsync();
            }

            return sequences[0];
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
