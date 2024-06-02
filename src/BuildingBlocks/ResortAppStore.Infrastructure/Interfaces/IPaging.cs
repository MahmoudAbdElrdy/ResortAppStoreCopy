namespace Common.Interfaces
{
  public interface IPaging
  {
    int PageIndex { get; set; }
    int PageSize { get; set; }
    string SortBy { get; set; }
    string SortOrder { get; set; }
    string Filter { get; set; }
  }
}