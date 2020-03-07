using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class BookInvalidEntityException : Exception
    {
        public BookInvalidEntityException(object entity, string message)
            : base($"Entity {entity.GetType().Name} state change rejected, {message}")
        {
        }
    }
}
