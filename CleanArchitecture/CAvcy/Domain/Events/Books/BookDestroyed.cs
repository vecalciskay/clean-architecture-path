using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Books
{
    public class BookDestroyed
    {
        public Guid BookId { get; set; }

        public BookDestroyed() { }
    }
}
