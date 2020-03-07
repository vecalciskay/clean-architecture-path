using Framework;
using System;

namespace Domain.ValueObjects
{
    public class Name200 : Value<Name200>
    {
        protected Name200() { }
        public string Value { get; set; }
        internal Name200(string title) => Value = title;
        public static Name200 FromString(string title)
        {
            CheckValidity(title);
            return new Name200(title);
        }

        private static void CheckValidity(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Name cannot be empty", nameof(title));
            if (title.Length > 200)
                throw new ArgumentOutOfRangeException(nameof(title), "Name cannot have more than 200 chars" );
        }

        public static implicit operator string(Name200 title) => title.Value;
    }
}
