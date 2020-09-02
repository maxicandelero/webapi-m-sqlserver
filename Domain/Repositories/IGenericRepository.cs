using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using webapi_m_sqlserver.Domain.Services.Communication;

namespace webapi_m_sqlserver.Domain.Repositories
{
    public interface IGenericRepository<TEntity, TFilter> where TEntity : class where TFilter : class
    {
        Task<ListAsyncResponse<TEntity>> ListAsync(TFilter filter);
        Task AddAsync(TEntity entity);
        Task<TEntity> FindByIdAsync(params object[] keyValues);
        void Update(TEntity entity);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity,bool>> predicate);
    }
}