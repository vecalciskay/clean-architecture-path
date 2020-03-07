using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Books
{
    public class BookDestroyed : IEvent
    {
        public Guid BookId { get; set; }

        public BookDestroyed() { }
    }
}
