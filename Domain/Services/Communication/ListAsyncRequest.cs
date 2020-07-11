using System.Collections.Generic;
using System.Linq;
using webapi_m_sqlserver.Domain.Models;

namespace webapi_m_sqlserver.Domain.Services.Communication
{
    public class ListAsyncRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string TextSearch { get; set; }
        public string OrderByColumn { get; set; }
        public bool OrderByDesc { get; set; }

        /// <summary>
        /// To add sort columns
        /// </summary>
        /// <typeparam name="SortModel"></typeparam>
        /// <returns></returns>
        private List<SortModel> _sortModels = new List<SortModel>();
        public List<SortModel> SortModels 
        { 
            get 
            {

                if (!string.IsNullOrEmpty(OrderByColumn) && !_sortModels.Any(x => x.OrderByColumn.Equals(OrderByColumn)))
                    _sortModels.Add(new SortModel{ OrderByColumn = OrderByColumn, OrderByDesc = OrderByDesc });
                return _sortModels;
            } 
            set { _sortModels = value; } 
        }
    }
}