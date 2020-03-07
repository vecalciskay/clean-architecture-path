using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Books
{
    public class BookCreated : IEvent
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }

        public BookCreated() { }
    }
}
