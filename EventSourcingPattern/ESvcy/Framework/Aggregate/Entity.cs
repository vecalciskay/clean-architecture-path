using System;

namespace Framework
{
    public abstract class Entity<TId> : IInternalEventHandler
        where TId : Value<TId>
    {
        private readonly Action<IEvent> _applier;

        public TId Id { get; protected set; }

        protected Entity(Action<IEvent> applier) => _applier = applier;

        protected Entity() { }

        protected abstract void When(object @event);

        protected void Apply(IEvent @event)
        {
            When(@event);
            _applier(@event);
        }

        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}
