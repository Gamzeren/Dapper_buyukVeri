namespace Dapper_buyukVeri.Dtos
{
    public class SalesByBrandDto
    {
        public string BrandName { get; set; }
        public int OrderCount { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalRevenue { get; set; }
        public double AvgOrderValue { get; set; }
    }
}
