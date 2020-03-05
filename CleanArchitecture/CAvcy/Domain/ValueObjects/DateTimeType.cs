using Framework;
using System;

namespace Domain.ValueObjects
{
    public class DateTimeType : Value<DateTimeType>, IEquatable<DateTimeType>
    {
        public DateTime Value { get; internal set; }

        protected DateTimeType() { }

        internal DateTimeType(DateTime dt) => Value = dt;

        public static DateTimeType FromDateTime(DateTime dt)
        {
            CheckValidity(dt);
            return new DateTimeType(dt);
        }

        public static DateTimeType Now(IDateService dateService) 
            => new DateTimeType(dateService.Now());

        public static void CheckValidity(DateTime dt)
        {
            if (dt == null)
                throw new ArgumentException("The date cannot be a null", nameof(dt));
            if (dt.CompareTo(new DateTime(1900, 1, 1)) < 0)
                throw new ArgumentException("The date cannot be before 01/01/1900", nameof(dt));
        }

        public bool Equals(DateTimeType other) => this.Value.Equals(other.Value);

        public static implicit operator DateTimeType(DateTime dt) 
            => new DateTimeType(dt);
        public static bool operator ==(DateTimeType d1, DateTimeType d2)
            => d1.Value == d2.Value;
        public static bool operator !=(DateTimeType d1, DateTimeType d2)
            => d1.Value != d2.Value;
        public static DateTimeType MinValue => new DateTimeType(new DateTime(1900, 1, 1));

        public override bool Equals(object obj)
        {
            if (obj is DateTimeType)
                return Equals((DateTimeType)obj);

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
