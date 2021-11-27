using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
