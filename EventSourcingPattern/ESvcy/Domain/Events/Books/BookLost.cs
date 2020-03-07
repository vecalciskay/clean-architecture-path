using Framework;
using System;

namespace Domain.Events.Books
{
    public class BookLost : IEvent
    {
        public Guid BookId { get; set; }

        public BookLost() { }
    }
}
