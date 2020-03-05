using Framework;
using System;

namespace Domain.ValueObjects
{
    public class BookReaderGuid : Value<BookReaderGuid>
    {
        public Guid Value { get; set; }

        protected BookReaderGuid() { }

        public BookReaderGuid(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id), "The id cannot be the default value");
            Value = id;
        }

        public static implicit operator Guid(BookReaderGuid id) => id.Value;

        public static implicit operator BookReaderGuid(string id) 
            => new BookReaderGuid(Guid.Parse(id));

        public static implicit operator BookReaderGuid(Guid id)
            => new BookReaderGuid(id);
    }
}
