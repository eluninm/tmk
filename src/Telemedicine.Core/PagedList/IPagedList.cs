using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Core.PagedList
{
    public interface IPagedList<T>
    {
        IEnumerable<T> Data { get; }

        int Page { get; }

        int PageSize { get; }

        int TotalCount { get; }
    }
}
