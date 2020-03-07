using Domain.Events.Readers;
using Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BookReader : AggregateRoot<BookReaderGuid>
    {
        public Guid BookReaderId { get; set; }
        public Name200 Name { get; set; }

        public BookReader() { }

        public BookReader(string name)
        {
            Apply(new ReaderCreated() { 
                BookReaderId = Guid.NewGuid(), 
                Name = name });
        }
        public void SetId()
        {
            this.Id = this.BookReaderId;
        }

        protected override void ValidateStatus()
        {
            ;
        }

        protected override void When(object @event)
        {
            switch(@event)
            {
                case Domain.Events.Readers.ReaderCreated e:
                    Id = new BookReaderGuid(e.BookReaderId); ;
                    BookReaderId = Id.Value;
                    Name = Name200.FromString(e.Name);
                    break;
            }
        }
    }
}
