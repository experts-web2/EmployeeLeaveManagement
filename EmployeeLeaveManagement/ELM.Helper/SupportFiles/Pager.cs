using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELM.Helper.SupportFiles
{
    public class Pager
    {
        public string search { get; set; } = String.Empty;

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
}
