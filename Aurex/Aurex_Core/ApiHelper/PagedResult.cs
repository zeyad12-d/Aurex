using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.ApiHelper
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public PagedResult(IEnumerable<T> data, int page, int pageSize, int totalCount)
        {
            Data = data;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
