using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Pagination
{
    public class Pager
    {
        public string search { get; set; } = String.Empty;
        public int Age { get; set; }

        public int MaxPagesize = 40;
        public int page { get; set; } = 1;

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
