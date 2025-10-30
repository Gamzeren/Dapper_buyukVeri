namespace Dapper_buyukVeri.Dtos
{
    public class SalesByAgeGroupDto
    {
        public string AgeGroup { get; set; }
        public int OrderCount { get; set; }
        public double TotalRevenue { get; set; }
        public double AvgOrderValue { get; set; }
    }
}
