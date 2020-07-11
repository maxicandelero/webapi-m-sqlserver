using System.Collections.Generic;

namespace webapi_m_sqlserver.Domain.Services.Communication
{
    public class ListAsyncResponse<T> where T : class
    {
        public int PageCount { get; set; }
        public int RowCount { get; set; } 
        public IEnumerable<T> Entities { get; set; } = new List<T>();  
    }
}