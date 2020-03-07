using System.Threading.Tasks;

namespace Framework
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}
