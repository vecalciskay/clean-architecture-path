using Framework;
using System;

namespace Domain.Events.Books
{
    public class BookLent : IEvent
    {
        public Guid BookLendId { get; set; }
        public Guid BookId { get; set; }
        public DateTime DateLent { get; set; }
        public BookReader Reader { get; set; }

        public BookLent() { }
    }
}
