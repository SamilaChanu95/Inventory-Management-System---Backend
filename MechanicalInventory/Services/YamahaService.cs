using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class YamahaService : IYamahaService
    {
        private readonly DataContext _dataContext;
        public YamahaService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddProduct(YamahaProduct yamahaProduct)
        {
            var sql = "INSERT INTO [yamahaProduct]([yamahaCode],[productName],[originCountry],[manufacturerYear],[packedOn],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@yamahaCode,@productName,@originCountry,@manufacturerYear,@packedOn,@quantity,@qualityLevel,@sellingPrice,@purchasePrice);";
            YamahaProduct product = new YamahaProduct() { YamahaCode = yamahaProduct.ProductCode, ProductName = yamahaProduct.ProductName, OriginCountry = yamahaProduct.OriginCountry, ManufacturerYear = yamahaProduct.ManufacturerYear, PackedOn = yamahaProduct.PackedOn, Quantity = yamahaProduct.Quantity, QualityLevel = yamahaProduct.QualityLevel, SellingPrice = yamahaProduct.SellingPrice, PurchasePrice = yamahaProduct.PurchasePrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = $"DELETE FROM [yamahaProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<YamahaProduct> GetProduct(int id)
        {
            var sql = $"SELECT *, [yamahaCode] AS productCode FROM [yamahaProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<YamahaProduct>(sql);
                return result;
            }
        }

        public async Task<List<YamahaProduct>> GetProductsList()
        {
            var sql = $"SELECT *, [yamahaCode] AS productCode FROM [yamahaProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<YamahaProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [yamahaProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateProduct(YamahaProduct yamahaProduct)
        {
            var sql = "UPDATE [yamahaProduct] SET [yamahaCode] = @yamahaCode, [productName] = @productName, [originCountry] = @originCountry, [manufacturerYear] = @manufacturerYear, [packedOn] = @packedOn, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            YamahaProduct product = new YamahaProduct() { Id = yamahaProduct.Id, YamahaCode = yamahaProduct.ProductCode, ProductName = yamahaProduct.ProductName, OriginCountry = yamahaProduct.OriginCountry, ManufacturerYear = yamahaProduct.ManufacturerYear, PackedOn = yamahaProduct.PackedOn, Quantity = yamahaProduct.Quantity, QualityLevel = yamahaProduct.QualityLevel, PurchasePrice = yamahaProduct.PurchasePrice, SellingPrice = yamahaProduct.SellingPrice };
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
