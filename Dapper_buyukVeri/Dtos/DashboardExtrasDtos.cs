namespace Dapper_buyukVeri.Dtos
{
    public class SalesPersonDto
    {
        public string NameSurname { get; set; }
        public int SalesCount { get; set; }
        public double TotalRevenue { get; set; }
    }
    public class CategorySalesDto
    {
        public string CategoryName { get; set; }
        public int SalesCount { get; set; }
        public double TotalRevenue { get; set; }
    }
    public class LastOrderDto
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string ItemName { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
    public class DailyRevenueDto
    {
        public DateTime Date { get; set; }
        public double Revenue { get; set; }
    }
    public class ActiveUserDto
    {
        public string UserName { get; set; }
        public int OrderCount { get; set; }
        public double TotalSpent { get; set; }
    }
    public class MonthlyRevenueDto
    {
        public string YearMonth { get; set; } // yyyy-MM
        public double Revenue { get; set; }
    }
}
