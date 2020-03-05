using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Books
{
    public class BookUpdated
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }

        public BookUpdated() { }
    }
}
