using System.Threading.Tasks;
using webapi_m_sqlserver.Domain.Services.Communication;

namespace webapi_m_sqlserver.Domain.Repositories
{
    public interface IGenericRepository<TEntity, TFilter> where TEntity : class where TFilter : class
    {
        Task<ListAsyncResponse<TEntity>> ListAsync(TFilter filter);
        Task AddAsync(TEntity entity);
        Task<TEntity> FindByIdAsync(long id);
        void Update(TEntity entity);
    }
}