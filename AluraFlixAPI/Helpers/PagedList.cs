

using System;
using System.Collections.Generic;
using System.Linq;

namespace AluraFlixAPI.Helpers
{
    public class PagedList<T> : List<T>
    {

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }


        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(List<T> items, int pageNumber, int pageSize)
        {
            int count = items.Count;
            List<T> list = items
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(list, count, pageNumber, pageSize);
        }
    }
}
