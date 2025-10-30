namespace Dapper_buyukVeri.Dtos
{
    public class DashboardStatsDto
    {
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public double TotalRevenue { get; set; }
        public double AvgOrderValue { get; set; }
        public DateTime LastOrderDate { get; set; }
        public int TotalCities { get; set; }
    }
}
