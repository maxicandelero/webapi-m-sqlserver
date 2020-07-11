using System.Threading.Tasks;

namespace webapi_m_sqlserver.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}