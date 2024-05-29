namespace Gestao.Client.Libraries.Utilities
{
    public class PaginatedList<T>
    {
        public PaginatedList(List<T> items, int pageIndex, int totalPages)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = totalPages;
        }

        public List<T> Items { get; } = new List<T>();
        public int PageIndex { get; }
        public int TotalPages { get; }
        public bool HasNextPage => PageIndex < TotalPages;
        public bool HasPreviousPage => PageIndex > 1;

    }
}
