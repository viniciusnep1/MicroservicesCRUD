using System.Linq;

namespace core.seedwork
{
    public class PaginateResult
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public IQueryable<dynamic> Items { get; set; }
    }
}
