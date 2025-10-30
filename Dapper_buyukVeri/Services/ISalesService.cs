using Dapper_buyukVeri.Dtos;

namespace Dapper_buyukVeri.Services
{
    public interface ISalesService
    {
        Task<double> GetTotalRevenueAsync();
        Task<int> GetTotalSalesCountAsync();
        Task<string> GetTopSalesCityAsync();
        Task<int> GetTopSalesYearAsync();
        Task<IList<(string Month, int OrderCount)>> GetMonthlyOrderCountsAsync();
        Task<IList<TopSellingProductDtos>> GetTopSellingProductsAsync(int topN = 5);
        Task<IList<SalesPersonDto>> GetTopSalesPersonsAsync(int topN = 5);
        Task<IList<CategorySalesDto>> GetTopCategoriesAsync(int topN = 10);
        Task<IList<LastOrderDto>> GetLastOrdersAsync(int topN = 5);
        Task<IList<DailyRevenueDto>> GetRecentEarningsAsync(int lastDays = 5);
        Task<IList<ActiveUserDto>> GetMostActiveUsersAsync(int topN = 5);
        Task<IList<MonthlyRevenueDto>> GetMonthlyRevenueAsync(int months);
        Task<PagedResultDto<SalesTableRowDto>> GetSalesTablePageAsync(SalesTableFilterDto filter);
    }
}
