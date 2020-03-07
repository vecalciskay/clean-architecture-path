using Framework;
using System;

namespace Infrastructure.Commands.V1.Books
{
    public class LendBook : ICommand
    {
        public Guid BookId { get; set; }
        public Guid BookReaderId { get; set; }
    }
}
