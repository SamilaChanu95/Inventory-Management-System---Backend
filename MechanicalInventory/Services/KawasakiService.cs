using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class KawasakiService : IKawasakiService
    {
        private readonly DataContext _dataContext;
        public KawasakiService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddProduct(KawasakiProduct kawasakiProduct)
        {
            var sql = "INSERT INTO [kawasakiProduct]([productCode],[productName],[manufacturerCountry],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@productCode,@productName,@manufacturerCountry,@manufacturerYear,@quantity,@qualityLevel,@sellingPrice,@purchasePrice);";
            KawasakiProduct product = new KawasakiProduct() { ProductCode = kawasakiProduct.ProductCode, ProductName = kawasakiProduct.ProductName, ManufacturerCountry = kawasakiProduct.ManufacturerCountry, ManufacturerYear = kawasakiProduct.ManufacturerYear, Quantity = kawasakiProduct.Quantity, QualityLevel = kawasakiProduct.QualityLevel, SellingPrice = kawasakiProduct.SellingPrice, PurchasePrice = kawasakiProduct.PurchasePrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = $"DELETE FROM [kawasakiProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<KawasakiProduct> GetProduct(int id)
        {
            var sql = $"SELECT * FROM [kawasakiProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<KawasakiProduct>(sql);
                return result;
            }
        }

        public async Task<List<KawasakiProduct>> GetProductsList()
        {
            var sql = $"SELECT * FROM [kawasakiProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<KawasakiProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [kawasakiProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateProduct(KawasakiProduct kawasakiProduct)
        {
            var sql = "UPDATE [kawasakiProduct] SET [productCode] = @productCode, [productName] = @productName, [manufacturerCountry] = @manufacturerCountry, [manufacturerYear] = @manufacturerYear, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            KawasakiProduct product = new KawasakiProduct() { Id = kawasakiProduct.Id, ProductCode = kawasakiProduct.ProductCode, ProductName = kawasakiProduct.ProductName, ManufacturerCountry = kawasakiProduct.ManufacturerCountry, ManufacturerYear = kawasakiProduct.ManufacturerYear, Quantity = kawasakiProduct.Quantity, QualityLevel = kawasakiProduct.QualityLevel, PurchasePrice = kawasakiProduct.PurchasePrice, SellingPrice = kawasakiProduct.SellingPrice };
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
