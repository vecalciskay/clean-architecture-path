using System;
using System.Diagnostics.CodeAnalysis;

namespace Framework.ValueObjects
{
    public class VersionNumber : Value<VersionNumber>
    {
        protected VersionNumber() { }

        public long Value { get; set; }

        internal VersionNumber(long aValue) => Value = aValue;

        public static VersionNumber FromLong(long aValue)
        {
            CheckValidity(aValue);
            return new VersionNumber(aValue);
        }

        private static void CheckValidity(long aValue)
        {
            if (aValue <= 0)
                throw new ArgumentException("A version number cannot be negative or 0");
        }

        public static implicit operator long(VersionNumber obj) => obj.Value;

        public static bool operator ==(VersionNumber o1, VersionNumber o2)
            => o1.Value == o2.Value;
        public static bool operator !=(VersionNumber o1, VersionNumber o2)
            => o1.Value != o2.Value;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (!(obj is VersionNumber))
                return false;

            return this.Value == ((VersionNumber)obj).Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
