using Framework;
using System;

namespace Domain.Events.Books
{
    public class BookReturned : IEvent
    {
        public DateTime DateReturned { get; set; }

        public BookReturned() { }
    }
}
