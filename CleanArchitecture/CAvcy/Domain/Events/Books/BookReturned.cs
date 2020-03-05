using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Books
{
    public class BookReturned
    {
        public DateTime DateReturned { get; set; }

        public BookReturned() { }
    }
}
