using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class HondaService : IHondaService
    {
        private readonly DataContext _dataContext;

        public HondaService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddProduct(HondaProduct hondaProduct)
        {
            var sql = "INSERT INTO [hondaProduct]([hondaCode],[productName],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@hondaCode, @productName, @manufacturerYear, @quantity, @qualityLevel, @sellingPrice, @purchasePrice);";
            HondaProduct product = new HondaProduct() { HondaCode = hondaProduct.ProductCode, ProductName = hondaProduct.ProductName, ManufacturerYear = hondaProduct.ManufacturerYear, Quantity = hondaProduct.Quantity, QualityLevel = hondaProduct.QualityLevel, PurchasePrice = hondaProduct.PurchasePrice, SellingPrice = hondaProduct.SellingPrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = $"DELETE FROM [hondaProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<HondaProduct> GetProduct(int id)
        {
            var sql = $"SELECT *, [hondaCode] AS ProductCode FROM [hondaProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                HondaProduct result = await connection.QueryFirstOrDefaultAsync<HondaProduct>(sql) ?? new HondaProduct() { Id = 0 };
                return result;
            }
        }

        public async Task<List<HondaProduct>> GetProductList()
        {
            var sql = $"SELECT *, [hondaCode] AS ProductCode FROM [hondaProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<HondaProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [hondaProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateProduct(HondaProduct hondaProduct)
        {
            var sql = "UPDATE [hondaProduct] SET [hondaCode] = @hondaCode, [productName] = @productName, [manufacturerYear] = @manufacturerYear, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            HondaProduct product = new HondaProduct() { Id = hondaProduct.Id, HondaCode = hondaProduct.ProductCode, ProductName = hondaProduct.ProductName, ManufacturerYear = hondaProduct.ManufacturerYear, Quantity = hondaProduct.Quantity, QualityLevel = hondaProduct.QualityLevel, PurchasePrice = hondaProduct.PurchasePrice, SellingPrice = hondaProduct.SellingPrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
