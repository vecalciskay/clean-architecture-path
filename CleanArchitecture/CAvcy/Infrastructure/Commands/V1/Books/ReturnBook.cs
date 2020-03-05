using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Commands.V1.Books
{
    public class ReturnBook
    {
        public Guid BookId { get; set; }
        public DateTime DateReturned { get; set; }
    }
}
