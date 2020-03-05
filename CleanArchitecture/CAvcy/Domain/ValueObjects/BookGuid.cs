using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class BookGuid : Value<BookGuid>
    {
        public Guid Value { get; set; }
        protected BookGuid() { }

        public BookGuid(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id), 
                    "The id cannot be the default value");
            Value = id;
        }

        public static implicit operator Guid(BookGuid id) => id.Value;

        public static implicit operator BookGuid(string id) 
            => new BookGuid(Guid.Parse(id));

        public static implicit operator BookGuid(Guid id)
            => new BookGuid(id);

        public static bool operator ==(BookGuid a, BookGuid b)
        {
            return a.Value.Equals(b.Value);
        }

        public static bool operator !=(BookGuid a, BookGuid b)
        {
            return !a.Value.Equals(b.Value);
        }
    }
}
