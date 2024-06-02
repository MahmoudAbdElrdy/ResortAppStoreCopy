using Common.Interfaces;

namespace Common.Infrastructures
{
    public class Paging : IPaging
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public string? Filter { get; set; }


    }
}