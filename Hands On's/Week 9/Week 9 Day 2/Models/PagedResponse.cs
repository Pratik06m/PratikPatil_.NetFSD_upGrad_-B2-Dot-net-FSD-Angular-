namespace Week9_Day2_ContactManagementAPI.Models
{
    public class PagedResponse<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public T? Data { get; set; }
    }
}
