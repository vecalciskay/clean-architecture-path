using Framework;
using System;

namespace Domain.ValueObjects
{
    public class Title500 : Value<Title500>
    {
        protected Title500() { }
        public string Value { get; set; }
        internal Title500(string title) => Value = title;
        public static Title500 FromString(string title)
        {
            CheckValidity(title);
            return new Title500(title);
        }

        private static void CheckValidity(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));
            if (title.Length > 500)
                throw new ArgumentOutOfRangeException(nameof(title), "Title cannot have more than 500 chars" );
        }

        public static implicit operator string(Title500 title) => title.Value;
    }
}
