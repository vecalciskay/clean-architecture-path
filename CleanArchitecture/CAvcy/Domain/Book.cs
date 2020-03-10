using Domain.Enums;
using Domain.Exceptions;
using Domain.Repository;
using Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Book : AggregateRoot<BookGuid>
    {
        public Guid BookId { get; private set; }
        public Guid CurrentBookReaderId { get; private set; }
        public Title500 Title { get; private set; }
        public StatusLent Status { get; private set; }
        public List<BookLend> HistoryLents { get; private set; }

        public byte[] TimeStamp { get; set; }
        public Book()
        {
            HistoryLents = new List<BookLend>();
        }
        public Book(string title)
        {
            HistoryLents = new List<BookLend>();
            Apply(new Domain.Events.Books.BookCreated() { 
                BookId = Guid.NewGuid(), Title = title 
            });
        }

        public void SetTitle(Title500 title)
        {
            Apply(new Domain.Events.Books.BookUpdated()
            {
                BookId = this.BookId,
                Title = title
            });
        }

        public void LendBook(BookReader reader, DateTime dateLent)
        {
            Apply(new Domain.Events.Books.BookLent()
            {
                BookId = this.BookId,
                BookLendId = Guid.NewGuid(),
                Reader = reader,
                DateLent = dateLent
            });
        }

        public void ReturnBook(DateTime dateReturned)
        {
            Apply(new Domain.Events.Books.BookReturned()
            {
                DateReturned = dateReturned
            }) ;
        }

        /// <summary>
        /// No need of extra check here, if it's saved in the repository, then it is ok
        /// </summary>
        /// <param name="lendRepository"></param>
        public void LoadLends(IBookLendsRepository lendRepository)
        {
            HistoryLents = lendRepository.LoadLends(BookId);
        }
        public BookLend LastLentWithNoReturn()
        {
            BookLend lastLend = null;
            if (HistoryLents.Count > 0)
                lastLend = HistoryLents.OrderBy(o => o.DateLent).Last();
            // No lend was made
            if (lastLend == null)
                return null;

            if (lastLend.DateReturned == DateTimeType.MinValue)
                return lastLend;

            return null;
        }

        protected override void ValidateStatus()
        {
            bool valid = BookId != null;
            switch(Status)
            {
                case StatusLent.Destroyed:
                case StatusLent.Available:
                    valid = valid
                        && CurrentBookReaderId == Guid.Empty
                        && LastLentWithNoReturn() == null;
                    break;
                case StatusLent.Lost:
                case StatusLent.Lent:
                    valid = valid
                        && CurrentBookReaderId != Guid.Empty
                        && LastLentWithNoReturn() != null;
                    break;
            }

            if (!valid)
                throw new BookInvalidEntityException(this, 
                    $"Post-checks failed in state {Status}");
        }

        protected override void When(object @event)
        {
            switch(@event)
            {
                case Domain.Events.Books.BookCreated e:
                    Id = new BookGuid(e.BookId);
                    BookId = Id.Value;
                    Title = Title500.FromString(e.Title);
                    CurrentBookReaderId = Guid.Empty;
                    Status = StatusLent.Available;
                    break;
                case Domain.Events.Books.BookLent e:
                    BookLend newLent = new BookLend(Apply);
                    ApplyToEntity(newLent, e);
                    HistoryLents.Add(newLent);
                    Status = StatusLent.Lent;
                    CurrentBookReaderId = newLent.BookReaderId;
                    break;
                case Domain.Events.Books.BookReturned e:
                    BookLend lastLent = HistoryLents.FindLast(o => o.DateReturned == DateTimeType.MinValue);
                    ApplyToEntity(lastLent, e);
                    Status = StatusLent.Available;
                    CurrentBookReaderId = Guid.Empty;
                    break;
                case Domain.Events.Books.BookLost e:
                    Status = StatusLent.Lost;
                    break;
                case Domain.Events.Books.BookDestroyed e:
                    Status = StatusLent.Destroyed;
                    break;
            }
        }
    }
}
