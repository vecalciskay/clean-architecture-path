using System.Threading.Tasks;

namespace Framework
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
