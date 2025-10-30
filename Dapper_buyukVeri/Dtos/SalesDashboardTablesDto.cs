namespace Dapper_buyukVeri.Dtos
{
    public class SalesDashboardTablesDto
    {
        public List<HighestOrderDto> HighestOrders { get; set; }
        public List<ActiveCustomerDto> ActiveCustomers { get; set; }
        public List<SalesByCategoryDto> SalesByCategory { get; set; }
        public List<TopSellingProductDto> TopProducts { get; set; }
    }

    public class HighestOrderDto
    {
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
    }

    public class ActiveCustomerDto
    {
        public string CustomerName { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
        public string City { get; set; }
    }

    public class SalesByCategoryDto
    {
        public string CategoryName { get; set; }
        public int SalesCount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class TopSellingProductDto
    {
        public int ItemId { get; set; }
        public string ProductName { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalRevenue { get; set; }
    }
}
