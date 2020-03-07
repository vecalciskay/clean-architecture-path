using Framework;
using System;

namespace Domain.Events.Books
{
    public class BookUpdated : IEvent
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }

        public BookUpdated() { }
    }
}
