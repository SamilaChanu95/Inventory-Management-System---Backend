using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class HeroService : IHeroService
    {
        private readonly DataContext _dataContext;

        public HeroService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddProduct(HeroProduct heroProduct)
        {
            var sql = "INSERT INTO [heroProduct]([heroCode],[productName],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@heroCode,@productName,@manufacturerYear,@quantity,@qualityLevel,@sellingPrice,@purchasePrice);";
            HeroProduct product = new HeroProduct() { HeroCode = heroProduct.HeroCode, ProductName = heroProduct.ProductName, ManufacturerYear = heroProduct.ManufacturerYear, Quantity = heroProduct.Quantity, QualityLevel = heroProduct.QualityLevel, PurchasePrice = heroProduct.PurchasePrice, SellingPrice = heroProduct.SellingPrice };
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = $"DELETE FROM [heroProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<HeroProduct> GetProduct(int id)
        {
            var sql = $"SELECT * FROM [heroProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                HeroProduct result = await connection.QueryFirstOrDefaultAsync<HeroProduct>(sql) ?? new HeroProduct() { Id = 0 };
                return result;
            }
        }

        public async Task<List<HeroProduct>> GetProductList()
        {
            var sql = $"SELECT * FROM [heroProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<HeroProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [heroProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateProduct(HeroProduct heroProduct)
        {
            var sql = "UPDATE [heroProduct] SET [heroCode] = @heroCode, [productName] = @productName, [manufacturerYear] = @manufacturerYear, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            HeroProduct product = new HeroProduct() { Id = heroProduct.Id, HeroCode = heroProduct.HeroCode, ProductName = heroProduct.ProductName, ManufacturerYear = heroProduct.ManufacturerYear, Quantity = heroProduct.Quantity, QualityLevel = heroProduct.QualityLevel, PurchasePrice = heroProduct.PurchasePrice, SellingPrice = heroProduct.SellingPrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
