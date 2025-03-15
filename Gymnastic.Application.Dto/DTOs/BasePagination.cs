namespace Gymnastic.Application.UseCases.Commons.Bases
{
    public class BasePagination<T>
    {
        public T? Data { get; set; }
        public int? PageNumber { get; set; }
        public int? TotalPages { get; set; }
        public int? TotalCount { get; set; }
        public bool? HasPreviousPage => PageNumber.HasValue ? PageNumber > 1 : null;
        public bool? HasNextPage => PageNumber.HasValue ? PageNumber < TotalPages : null;
    }
}
