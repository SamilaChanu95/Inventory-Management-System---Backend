using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class KtmService : IKtmService
    {
        private readonly DataContext _dataContext;
        public KtmService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddProduct(KtmProduct ktmProduct)
        {
            var sql = "INSERT INTO [ktmProduct]([productCode],[productName],[manufacturerCountry],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@productCode,@productName,@manufacturerCountry,@manufacturerYear,@quantity,@qualityLevel,@sellingPrice,@purchasePrice);";
            KtmProduct product = new KtmProduct() { ProductCode = ktmProduct.ProductCode, ProductName = ktmProduct.ProductName, ManufacturerCountry = ktmProduct.ManufacturerCountry, ManufacturerYear = ktmProduct.ManufacturerYear, Quantity = ktmProduct.Quantity, QualityLevel = ktmProduct.QualityLevel, SellingPrice = ktmProduct.SellingPrice, PurchasePrice = ktmProduct.PurchasePrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = $"DELETE FROM [ktmProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<KtmProduct> GetProduct(int id)
        {
            var sql = $"SELECT * FROM [ktmProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<KtmProduct>(sql);
                return result;
            }
        }

        public async Task<List<KtmProduct>> GetProductsList()
        {
            var sql = $"SELECT * FROM [ktmProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<KtmProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [ktmProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateProduct(KtmProduct ktmProduct)
        {
            var sql = "UPDATE [ktmProduct] SET [productCode] = @productCode, [productName] = @productName, [manufacturerCountry] = @manufacturerCountry, [manufacturerYear] = @manufacturerYear, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            KtmProduct product = new KtmProduct() { Id = ktmProduct.Id, ProductCode = ktmProduct.ProductCode, ProductName = ktmProduct.ProductName, ManufacturerCountry = ktmProduct.ManufacturerCountry, ManufacturerYear = ktmProduct.ManufacturerYear, Quantity = ktmProduct.Quantity, QualityLevel = ktmProduct.QualityLevel, PurchasePrice = ktmProduct.PurchasePrice, SellingPrice = ktmProduct.SellingPrice };
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
