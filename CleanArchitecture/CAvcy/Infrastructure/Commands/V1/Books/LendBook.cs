using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Commands.V1.Books
{
    public class LendBook
    {
        public Guid BookId { get; set; }
        public Guid BookReaderId { get; set; }
    }
}
