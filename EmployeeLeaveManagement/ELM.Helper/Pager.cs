
namespace ELM.Helper;
    public class Pager
    {
        public string Search { get; set; } = String.Empty;
    public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public int MaxPagesize = 40;
        public int CurrentPage { get; set; } = 1;
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public int TotalPages { get; set; }

        private int _PageSize = 5;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = (value > MaxPagesize) ? MaxPagesize : value;
            }
        }
    }
