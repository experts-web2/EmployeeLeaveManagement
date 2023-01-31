using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Pagination
{
   public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;


        public PagedList(List<T> items, int count, int PageNumber, int Pagesize)
        {
            TotalCount = count;
            CurrentPage = PageNumber;
            PageSize = Pagesize;
            TotalPages = (int)Math.Ceiling(count / (double)Pagesize);
            AddRange(items);
        }
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pagenumber, int pagesize)
        {
            var count = source.Count();
            var items = source.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToList();
            return new PagedList<T>(items, count, pagenumber, pagesize);
        }
    }
}
