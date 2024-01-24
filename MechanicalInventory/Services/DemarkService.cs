using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class DemarkService : IDemarkService
    {
        private readonly DataContext _dataContext;
        public DemarkService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddProduct(DemarkProduct demarkProduct)
        {
            var sql = "INSERT INTO [demarkProduct]([productCode],[productName],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@productCode,@productName,@manufacturerYear,@quantity,@qualityLevel,@sellingPrice,@purchasePrice);";
            DemarkProduct product = new DemarkProduct() { ProductCode = demarkProduct.ProductCode, ProductName = demarkProduct.ProductName, ManufacturerYear = demarkProduct.ManufacturerYear, Quantity = demarkProduct.Quantity, QualityLevel = demarkProduct.QualityLevel, SellingPrice = demarkProduct.SellingPrice, PurchasePrice = demarkProduct.PurchasePrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = $"DELETE FROM [demarkProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<DemarkProduct> GetProduct(int id)
        {
            var sql = $"SELECT * FROM [demarkProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<DemarkProduct>(sql);
                return result;
            }
        }

        public async Task<List<DemarkProduct>> GetProductsList()
        {
            var sql = $"SELECT * FROM [demarkProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<DemarkProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [demarkProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateProduct(DemarkProduct demarkProduct)
        {
            var sql = "UPDATE [demarkProduct] SET [productCode] = @productCode, [productName] = @productName, [manufacturerYear] = @manufacturerYear, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            DemarkProduct product = new DemarkProduct() { Id = demarkProduct.Id, ProductCode = demarkProduct.ProductCode, ProductName = demarkProduct.ProductName, ManufacturerYear = demarkProduct.ManufacturerYear, Quantity = demarkProduct.Quantity, QualityLevel = demarkProduct.QualityLevel, PurchasePrice = demarkProduct.PurchasePrice, SellingPrice = demarkProduct.SellingPrice };
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
