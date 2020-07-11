using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Domain.Services.Communication;

namespace webapi_m_sqlserver.Persistence.Repositories
{
    public static class IQueryableExtension
    {

        public static async Task<ListAsyncResponse<T>> GetPaged<T>(this IQueryable<T> query, int page, int pageSize, IEnumerable<SortModel> sortModels = null) where T : class
        {
            // Valido SortModels
            if (sortModels != null && sortModels.Count() > 0)
            {
                try 
                {
                    var expression = query.Expression;
                    int count = 0;
                    foreach (var item in sortModels)
                    {
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var selector = Expression.PropertyOrField(parameter, item.OrderByColumn);
                        var method = item.OrderByDesc ?
                            (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                            (count == 0 ? "OrderBy" : "ThenBy");
                        expression = Expression.Call(typeof(Queryable), method,
                            new Type[] { query.ElementType, selector.Type },
                            expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                        count++;
                    }
                    query = query.Provider.CreateQuery<T>(expression);
                }
                catch 
                {
                }
            }
            
            var result = new ListAsyncResponse<T>();
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            if (page == -1)
                result.Entities = await query.ToListAsync();
            else
            {
                var skip = (page - 1) * pageSize;     
                result.Entities = await query.Skip(skip).Take(pageSize).ToListAsync();
            }
        
            return result;
        }
    }
}