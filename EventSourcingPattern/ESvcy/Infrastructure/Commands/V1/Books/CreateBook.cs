using Framework;

namespace Infrastructure.Commands.V1.Books
{
    public class CreateBook : ICommand
    {
        public string Title { get; set; }
    }
}
