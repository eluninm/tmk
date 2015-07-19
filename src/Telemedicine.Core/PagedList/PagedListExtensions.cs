using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Core.PagedList
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, int page, int pageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var pagingConfig = new PagingConfig(page, pageSize);
            var skipCount = ValidatePagePropertiesAndGetSkipCount(pagingConfig);

            var data = query
                .Skip(skipCount)
                .Take(pagingConfig.PageSize)
                .ToList();

            if (skipCount > 0 && data.Count == 0)
            {
                // Requested page has no records, just return the first page
                pagingConfig.Page = 1;
                data = query
                    .Take(pagingConfig.PageSize)
                    .ToList();
            }

            return new PagedList<T>(data, pagingConfig.Page, pagingConfig.PageSize, query.Count());
        }

        public static async Task<IPagedList<TModel>> ToPagedListAsync<TModel>(this IQueryable<TModel> query, int page, int pageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var pagingConfig = new PagingConfig(page, pageSize);
            var skipCount = ValidatePagePropertiesAndGetSkipCount(pagingConfig);

            var data = await query
                .Skip(skipCount)
                .Take(pagingConfig.PageSize)
                .ToListAsync();

            if (skipCount > 0 && data.Count == 0)
            {
                // Requested page has no records, just return the first page
                pagingConfig.Page = 1;
                data = await query.Take(pagingConfig.PageSize).ToListAsync();
            }

            var count = query.Count();
            return new PagedList<TModel>(data, pagingConfig.Page, pagingConfig.PageSize, count);
        }

        private static int ValidatePagePropertiesAndGetSkipCount(PagingConfig pagingConfig)
        {
            if (pagingConfig.Page < 1)
            {
                pagingConfig.Page = 1;
            }

            if (pagingConfig.PageSize < 10)
            {
                pagingConfig.PageSize = 10;
            }

            if (pagingConfig.PageSize > 100)
            {
                pagingConfig.PageSize = 100;
            }

            return pagingConfig.PageSize * (pagingConfig.Page - 1);
        }

        internal class PagingConfig
        {
            public PagingConfig(int page, int pageSize)
            {
                Page = page;
                PageSize = pageSize;
            }

            public int Page { get; set; }

            public int PageSize { get; set; }
        }
    }
}
