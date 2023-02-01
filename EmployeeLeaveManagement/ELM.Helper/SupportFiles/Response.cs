
using ELM.Helper.SupportFiles;

namespace ELM.Web.Helper
{
    public class Response<T>
    {
        public List<T> DataList { get; set; } = new();
        public Pager Pager { get; set; } = new();
    }
}
