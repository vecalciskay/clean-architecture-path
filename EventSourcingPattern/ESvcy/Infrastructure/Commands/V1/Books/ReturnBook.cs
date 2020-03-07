using Framework;
using System;

namespace Infrastructure.Commands.V1.Books
{
    public class ReturnBook : ICommand
    {
        public Guid BookId { get; set; }
        public DateTime DateReturned { get; set; }
    }
}
