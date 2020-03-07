using Framework;

namespace Infrastructure.Commands.V1.BookReaders
{
    public class CreateReader : ICommand
    {
        public string Name { get; set; }
    }
}
