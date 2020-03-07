using Framework.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Framework
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler
        where TId : Value<TId>
    {
        public TId Id { get; protected set; }
        public VersionNumber Version { get; set; }

        protected abstract void When(object @event);

        private readonly List<IEvent> _changes;

        protected AggregateRoot() => _changes = new List<IEvent>();

        protected void Apply(IEvent @event)
        {
            When(@event);
            ValidateStatus();
            _changes.Add(@event);
        }

        public IEnumerable<IEvent> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();

        protected abstract void ValidateStatus();

        protected void ApplyToEntity(IInternalEventHandler entity, object @event)
            => entity?.Handle(@event);

        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}
