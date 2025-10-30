using Dapper;
using Dapper_buyukVeri.Context;
using Dapper_buyukVeri.Dtos;

namespace Dapper_buyukVeri.Services
{
    public class SalesService : ISalesService
    {
        private readonly DapperContext _context;

        public SalesService(DapperContext context)
        {
            _context = context;
            System.Diagnostics.Debug.WriteLine("[DEBUG] ConnectionString: " + _context.GetConnectionString());
        }

        public async Task<double> GetTotalRevenueAsync()
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = "SELECT SUM(TOTALPRICE) FROM [dbo].[SALES]";
                var result = await connection.ExecuteScalarAsync<double?>(sql);
                return result ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<int> GetTotalSalesCountAsync()
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM [dbo].[SALES]";
                var result = await connection.ExecuteScalarAsync<int?>(sql);
                return result ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<string> GetTopSalesCityAsync()
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"SELECT TOP 1 CITY FROM [dbo].[SALES] GROUP BY CITY ORDER BY SUM(TOTALPRICE) DESC";
                var result = await connection.ExecuteScalarAsync<string>(sql);
                return result ?? "-";
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<int> GetTopSalesYearAsync()
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"SELECT TOP 1 YEAR(DATE_) FROM [dbo].[SALES] GROUP BY YEAR(DATE_) ORDER BY SUM(TOTALPRICE) DESC";
                var result = await connection.ExecuteScalarAsync<int?>(sql);
                return result ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<IList<(string Month, int OrderCount)>> GetMonthlyOrderCountsAsync()
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"SELECT FORMAT(DATE_, 'yyyy-MM') AS [Month], COUNT(*) AS [OrderCount] FROM [dbo].[SALES] GROUP BY FORMAT(DATE_, 'yyyy-MM') ORDER BY [Month]";
                var list = await connection.QueryAsync<(string, int)>(sql);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<IList<TopSellingProductDtos>> GetTopSellingProductsAsync(int topN = 5)
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = $@"SELECT TOP (@topN) ITEMID as ItemId, ITEMNAME as ProductName, SUM(AMOUNT) as TotalQuantity, SUM(TOTALPRICE) as TotalRevenue FROM [dbo].[SALES] GROUP BY ITEMID, ITEMNAME ORDER BY SUM(AMOUNT) DESC";
                var list = await connection.QueryAsync<TopSellingProductDtos>(sql, new { topN });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<IList<SalesPersonDto>> GetTopSalesPersonsAsync(int topN = 5)
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"SELECT TOP (@topN)
                                NAMESURNAME AS NameSurname,
                                COUNT(*) AS SalesCount,
                                SUM(TOTALPRICE) AS TotalRevenue
                               FROM [dbo].[SALES]
                               GROUP BY NAMESURNAME
                               ORDER BY SUM(TOTALPRICE) DESC";
                var list = await connection.QueryAsync<SalesPersonDto>(sql, new { topN });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<IList<CategorySalesDto>> GetTopCategoriesAsync(int topN = 10)
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"SELECT TOP (@topN)
                                CATEGORY1 AS CategoryName,
                                COUNT(*) AS SalesCount,
                                SUM(TOTALPRICE) AS TotalRevenue
                               FROM [dbo].[SALES]
                               GROUP BY CATEGORY1
                               ORDER BY SUM(TOTALPRICE) DESC";
                var list = await connection.QueryAsync<CategorySalesDto>(sql, new { topN });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<IList<LastOrderDto>> GetLastOrdersAsync(int topN = 5)
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"SELECT TOP (@topN)
                                ORDERID AS OrderId,
                                USERNAME_ AS UserName,
                                ITEMNAME AS ItemName,
                                TOTALPRICE AS TotalPrice,
                                DATE_ AS [Date]
                               FROM [dbo].[SALES]
                               ORDER BY DATE_ DESC";
                var list = await connection.QueryAsync<LastOrderDto>(sql, new { topN });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<IList<DailyRevenueDto>> GetRecentEarningsAsync(int lastDays = 5)
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"SELECT CONVERT(date, DATE_) AS [Date], SUM(TOTALPRICE) AS Revenue
                               FROM [dbo].[SALES]
                               WHERE DATE_ >= DATEADD(day, -@lastDays, CAST(GETDATE() AS date))
                               GROUP BY CONVERT(date, DATE_)
                               ORDER BY [Date] DESC";
                var list = await connection.QueryAsync<DailyRevenueDto>(sql, new { lastDays });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<IList<ActiveUserDto>> GetMostActiveUsersAsync(int topN = 5)
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"SELECT TOP (@topN)
                                USERNAME_ AS UserName,
                                COUNT(*) AS OrderCount,
                                SUM(TOTALPRICE) AS TotalSpent
                               FROM [dbo].[SALES]
                               GROUP BY USERNAME_
                               ORDER BY COUNT(*) DESC";
                var list = await connection.QueryAsync<ActiveUserDto>(sql, new { topN });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<IList<MonthlyRevenueDto>> GetMonthlyRevenueAsync(int months)
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            try
            {
                connection.Open();
                string sql = @"WITH m AS (
                                SELECT FORMAT(DATE_, 'yyyy-MM') AS YM, SUM(TOTALPRICE) AS Revenue
                                FROM [dbo].[SALES]
                                WHERE DATE_ >= DATEADD(month, -@months, CAST(GETDATE() AS date))
                                GROUP BY FORMAT(DATE_, 'yyyy-MM')
                              )
                              SELECT YM AS YearMonth, Revenue
                              FROM m
                              ORDER BY YM";
                var list = await connection.QueryAsync<MonthlyRevenueDto>(sql, new { months });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SQL açılırken hata: " + ex.Message, ex);
            }
        }
        public async Task<PagedResultDto<SalesTableRowDto>> GetSalesTablePageAsync(SalesTableFilterDto filter)
        {
            var cs = _context.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs))
                throw new Exception("Connection string boş/null. appsettings.json -> ConnectionStrings:SqlConnection kontrol edin.");
            using var connection = new System.Data.SqlClient.SqlConnection(cs);
            connection.Open();

            var where = new System.Text.StringBuilder("WHERE 1=1 ");
            var p = new Dapper.DynamicParameters();

            if (filter.DateFrom.HasValue)
            {
                where.Append(" AND DATE_ >= @DateFrom");
                p.Add("DateFrom", filter.DateFrom.Value.Date);
            }
            if (filter.DateTo.HasValue)
            {
                where.Append(" AND DATE_ < DATEADD(day,1,@DateTo)");
                p.Add("DateTo", filter.DateTo.Value.Date);
            }
            if (!string.IsNullOrWhiteSpace(filter.City))
            {
                where.Append(" AND CITY = @City");
                p.Add("City", filter.City);
            }
            if (!string.IsNullOrWhiteSpace(filter.Category))
            {
                where.Append(" AND CATEGORY1 = @Category");
                p.Add("Category", filter.Category);
            }
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                where.Append(" AND (ITEMNAME LIKE @Search OR USERNAME_ LIKE @Search)");
                p.Add("Search", "%" + filter.Search + "%");
            }

            int skip = (filter.Page - 1) * filter.PageSize;
            p.Add("Skip", skip);
            p.Add("Take", filter.PageSize);

            string baseSelect = @"SELECT ORDERID as OrderId, DATE_ as [Date], USERNAME_ as UserName, CITY, CATEGORY1 as Category, ITEMNAME as ItemName, AMOUNT, TOTALPRICE FROM [dbo].[SALES] ";
            string sql = $@"{baseSelect} {where} ORDER BY DATE_ DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY; ";
            string countSql = $@"SELECT COUNT(1) FROM [dbo].[SALES] {where};";

            var items = await connection.QueryAsync<SalesTableRowDto>(sql, p);
            var total = await connection.ExecuteScalarAsync<int>(countSql, p);

            return new PagedResultDto<SalesTableRowDto> { Items = items, TotalCount = total };
        }
    }
}
