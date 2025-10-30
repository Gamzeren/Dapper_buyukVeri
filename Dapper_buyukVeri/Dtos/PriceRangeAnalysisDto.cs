namespace Dapper_buyukVeri.Dtos
{
    public class PriceRangeAnalysisDto
    {
        public string PriceRange { get; set; }
        public double TotalQuantity { get; set; }
        public int OrderCount { get; set; }
        public double AvgQuantityPerOrder { get; set; }
        public double TotalRevenue { get; set; }
    }
}
