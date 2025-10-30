namespace Dapper_buyukVeri.Dtos
{
    public class SalesTableRowDto
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public string ItemName { get; set; }
        public double Amount { get; set; }
        public double TotalPrice { get; set; }
    }

    public class SalesTableFilterDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? City { get; set; }
        public string? Category { get; set; }
    }

    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
