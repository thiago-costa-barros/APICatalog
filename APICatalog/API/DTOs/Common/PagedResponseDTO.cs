using APICatalog.Core.Common.Pagination;

namespace APICatalog.API.DTOs.Common
{
    public class PagedResponseDTO<T> where T : class
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public IEnumerable<T> Items { get; set; }

        public PagedResponseDTO(PagedList<T> pagedList)
        {
            TotalCount = pagedList.TotalCount;
            TotalPages = pagedList.TotalPages;
            PageSize = pagedList.PageSize;
            CurrentPage = pagedList.CurrentPage;
            HasPrevious = pagedList.HasPrevious;
            HasNext = pagedList.HasNext;
            Items = pagedList;
        }
    }
}
