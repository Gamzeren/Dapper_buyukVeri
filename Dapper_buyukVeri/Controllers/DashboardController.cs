using Microsoft.AspNetCore.Mvc;
using Dapper_buyukVeri.Services;
using Dapper_buyukVeri.Dtos;

namespace Dapper_buyukVeri.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ISalesService _salesService;
        public DashboardController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        public async Task<IActionResult> Index()
        {
            var totalRevenue = await _salesService.GetTotalRevenueAsync();
            var totalSalesCount = await _salesService.GetTotalSalesCountAsync();
            var topSalesCity = await _salesService.GetTopSalesCityAsync();
            var topSalesYear = await _salesService.GetTopSalesYearAsync();
            var monthlyOrderCounts = await _salesService.GetMonthlyOrderCountsAsync();
            var topProducts = await _salesService.GetTopSellingProductsAsync(6);

            var topSalesPersons = await _salesService.GetTopSalesPersonsAsync(5);
            var topCategories = await _salesService.GetTopCategoriesAsync(5);
            var lastOrders = await _salesService.GetLastOrdersAsync(5);
            var activeUsers = await _salesService.GetMostActiveUsersAsync(5);

            var vm = new DashboardViewModel
            {
                TotalRevenue = totalRevenue,
                TotalSalesCount = totalSalesCount,
                TopSalesCity = topSalesCity,
                TopSalesYear = topSalesYear,
                MonthlyOrderCounts = monthlyOrderCounts.ToList(),
                TopProducts = topProducts.ToList(),
                TopSalesPersons = topSalesPersons.ToList(),
                TopCategories = topCategories.ToList(),
                LastOrders = lastOrders.ToList(),
                ActiveUsers = activeUsers.ToList()
            };
            return View(vm);
        }

        public IActionResult Table()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TableData([FromQuery] SalesTableFilterDto filter)
        {
            if (filter.Page <= 0) filter.Page = 1;
            if (filter.PageSize <= 0 || filter.PageSize > 100) filter.PageSize = 10;
            var result = await _salesService.GetSalesTablePageAsync(filter);
            return Json(result);
        }
    }
    public class DashboardViewModel
    {
        public double TotalRevenue { get; set; }
        public int TotalSalesCount { get; set; }
        public string TopSalesCity { get; set; }
        public int TopSalesYear { get; set; }
        public List<(string Month, int OrderCount)> MonthlyOrderCounts { get; set; }
        public List<TopSellingProductDtos> TopProducts { get; set; }
        public List<SalesPersonDto> TopSalesPersons { get; set; }
        public List<CategorySalesDto> TopCategories { get; set; }
        public List<LastOrderDto> LastOrders { get; set; }
        public List<ActiveUserDto> ActiveUsers { get; set; }
    }
}
