using Framework;
using System;

namespace Domain.Events.Readers
{
    public class ReaderCreated : IEvent
    {
        public Guid BookReaderId { get; set; }
        public string Name { get; set; }
    }
}
