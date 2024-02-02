using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class SuzukiService : ISuzukiService
    {
        private readonly DataContext _dataContext;
        public SuzukiService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddProduct(SuzukiProduct suzukiProduct)
        {
            var sql = "INSERT INTO [suzukiProduct]([productCode],[productName],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@productCode,@productName,@manufacturerYear,@quantity,@qualityLevel,@sellingPrice,@purchasePrice);";
            SuzukiProduct product = new SuzukiProduct() { ProductCode = suzukiProduct.ProductCode, ProductName = suzukiProduct.ProductName, ManufacturerYear = suzukiProduct.ManufacturerYear, Quantity = suzukiProduct.Quantity, QualityLevel = suzukiProduct.QualityLevel, SellingPrice = suzukiProduct.SellingPrice, PurchasePrice = suzukiProduct.PurchasePrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = $"DELETE FROM [suzukiProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<SuzukiProduct> GetProduct(int id)
        {
            var sql = $"SELECT * FROM [suzukiProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<SuzukiProduct>(sql);
                return result;
            }
        }

        public async Task<List<SuzukiProduct>> GetProductsList()
        {
            var sql = $"SELECT * FROM [suzukiProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<SuzukiProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [suzukiProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateProduct(SuzukiProduct suzukiProduct)
        {
            var sql = "UPDATE [suzukiProduct] SET [productCode] = @productCode, [productName] = @productName, [manufacturerYear] = @manufacturerYear, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            SuzukiProduct product = new SuzukiProduct() { Id = suzukiProduct.Id, ProductCode = suzukiProduct.ProductCode, ProductName = suzukiProduct.ProductName, ManufacturerYear = suzukiProduct.ManufacturerYear, Quantity = suzukiProduct.Quantity, QualityLevel = suzukiProduct.QualityLevel, PurchasePrice = suzukiProduct.PurchasePrice, SellingPrice = suzukiProduct.SellingPrice };
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
