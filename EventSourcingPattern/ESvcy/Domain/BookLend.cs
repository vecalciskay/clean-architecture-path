using Domain.ValueObjects;
using Framework;
using System;

namespace Domain
{
    public class BookLend : Entity<LendGuid>
    {
        public Guid BookLendId { get; internal set; }
        public BookGuid BookId { get; set; }
        public Book BookRef { get; set; }
        public BookReaderGuid BookReaderId { get; set; }
        public BookReader ReaderRef { get; set; }
        public DateTimeType DateLent { get; set; }
        public DateTimeType DateReturned { get; set; }

        public BookLend() { }
        public BookLend(Action<IEvent> applier) : base(applier)
        {
        }
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Domain.Events.Books.BookLent e:
                    Id = new LendGuid(e.BookLendId);
                    BookLendId = Id.Value;
                    BookId = e.BookId;
                    DateLent = e.DateLent;
                    BookReaderId = e.Reader.BookReaderId;
                    ReaderRef = e.Reader;
                    DateReturned = DateTimeType.MinValue;
                    break;
                case Domain.Events.Books.BookReturned e:
                    DateReturned = e.DateReturned;
                    break;
            }
        }
    }
}
