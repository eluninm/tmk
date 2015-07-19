using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Core.PagedList
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList(IEnumerable<T> data, int page, int pageSize, int totalCount)
        {
            Data = data;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public IEnumerable<T> Data { get; private set; }

        public int Page { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPages => TotalCount/PageSize + TotalCount%PageSize > 0 ? 1 : 0;
    }
}
