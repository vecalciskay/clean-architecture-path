using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Books
{
    public class BookCreated
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }

        public BookCreated() { }
    }
}
