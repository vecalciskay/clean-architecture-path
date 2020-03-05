using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class LendGuid : Value<LendGuid>
    {
        public Guid Value { get; set; }

        protected LendGuid() { }

        public LendGuid(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id), "The id cannot be the default value");
            Value = id;
        }

        public static implicit operator Guid(LendGuid id) => id.Value;

        public static implicit operator LendGuid(Guid id) => new LendGuid(id);

        public static implicit operator LendGuid(string id) 
            => new LendGuid(Guid.Parse(id));
    }
}
