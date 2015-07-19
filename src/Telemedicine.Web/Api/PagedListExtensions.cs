using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Web.Api
{
    public static class PagedListExtensions
    {
        public static IPagedList<TDto> Map<TModel, TDto>(this IPagedList<TModel> list,
            Func<TModel, TDto> mappingFunc)
        {
            var newDataList = list.Data.Select(mappingFunc).ToList();
            var newList = new PagedList<TDto>(newDataList, list.Page, list.PageSize, list.TotalCount);
            return newList;
        } 
    }
}