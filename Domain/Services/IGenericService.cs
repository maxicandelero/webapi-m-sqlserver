using System.Threading.Tasks;
using webapi_m_sqlserver.Domain.Services.Communication;

namespace webapi_m_sqlserver.Domain.Services
{
    public interface IGenericService<TEntity, TListRequest, TFindByIdRequest> where TEntity : class where TListRequest : class where TFindByIdRequest : class
    {
         Task<ListAsyncResponse<TEntity>> ListAsync(TListRequest filter);
         Task<GenericResponse<TEntity>> SaveAsync(TEntity entity);
         Task<GenericResponse<TEntity>> FindByIdAsync(TFindByIdRequest filter);
    }
}