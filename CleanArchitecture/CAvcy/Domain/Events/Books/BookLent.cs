using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Books
{
    public class BookLent
    {
        public Guid BookLendId { get; set; }
        public Guid BookId { get; set; }
        public DateTime DateLent { get; set; }
        public BookReader Reader { get; set; }

        public BookLent() { }
    }
}
