using X.PagedList;

namespace Common.Infrastructures {

  public class PageList {
    public PagedListMetaData Metadata { get; set; }
  }
  public  class PageList<T>:PageList {

      public IPagedList<T> Items { get; set; }
    public PageList(IPagedList<T> listPage) {
      Items = listPage;
      Metadata = listPage.GetMetaData();
    }
    
  }
    public class PaginatedList<T>
    {
        #region Properties

        public int PageIndex { get; }

        public int TotalPages { get; }

        public int TotalCount { get; }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);
        public List<T> Items { get; set; }
        #endregion

        #region Ctor

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize) > 0 ? (int)Math.Ceiling(count / (double)pageSize) : 1;
            TotalCount = count;
            Items = new List<T>();
            Items.AddRange(items);
        }

        #endregion
    }
}