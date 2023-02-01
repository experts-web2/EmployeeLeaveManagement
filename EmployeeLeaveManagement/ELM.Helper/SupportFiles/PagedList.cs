namespace ELM.Helper.SupportFiles
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
            var items = source.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToList();
            var count = source.Count();
            return new PagedList<T>(items, count, pagenumber, pagesize);
        }
    }
}
