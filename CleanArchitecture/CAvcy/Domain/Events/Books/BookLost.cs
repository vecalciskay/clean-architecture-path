using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Books
{
    public class BookLost
    {
        public Guid BookId { get; set; }

        public BookLost() { }
    }
}
