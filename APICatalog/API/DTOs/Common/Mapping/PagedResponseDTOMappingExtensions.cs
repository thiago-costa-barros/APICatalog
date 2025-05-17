using APICatalog.Core.Common.Pagination;

namespace APICatalog.API.DTOs.Common.Mapping
{
    public static class PagedResponseDTOMappingExtensions
    {
        public static PagedResponseDTO<T>? MapToPagedResponseDTO<T>(this PagedList<T> pagedList) where T : class
        {
            return pagedList is null ? null : new PagedResponseDTO<T>(pagedList);
        }
    }
}
