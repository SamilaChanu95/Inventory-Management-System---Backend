using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class MahindraService : IMahindraService
    {
        private readonly DataContext _dataContext;
        public MahindraService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddProduct(MahindraProduct mahindraProduct)
        {
            var sql = "INSERT INTO [mahindraProduct]([productCode],[productName],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@productCode,@productName,@manufacturerYear,@quantity,@qualityLevel,@sellingPrice,@purchasePrice);";
            MahindraProduct product = new MahindraProduct() { ProductCode = mahindraProduct.ProductCode, ProductName = mahindraProduct.ProductName, ManufacturerYear = mahindraProduct.ManufacturerYear, Quantity = mahindraProduct.Quantity, QualityLevel = mahindraProduct.QualityLevel, SellingPrice = mahindraProduct.SellingPrice, PurchasePrice = mahindraProduct.PurchasePrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = $"DELETE FROM [mahindraProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<MahindraProduct> GetProduct(int id)
        {
            var sql = $"SELECT * FROM [mahindraProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<MahindraProduct>(sql);
                return result;
            }
        }

        public async Task<List<MahindraProduct>> GetProductsList()
        {
            var sql = $"SELECT * FROM [mahindraProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<MahindraProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [mahindraProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateProduct(MahindraProduct mahindraProduct)
        {
            var sql = "UPDATE [mahindraProduct] SET [productCode] = @productCode, [productName] = @productName, [manufacturerYear] = @manufacturerYear, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            MahindraProduct product = new MahindraProduct() { Id = mahindraProduct.Id, ProductCode = mahindraProduct.ProductCode, ProductName = mahindraProduct.ProductName, ManufacturerYear = mahindraProduct.ManufacturerYear, Quantity = mahindraProduct.Quantity, QualityLevel = mahindraProduct.QualityLevel, PurchasePrice = mahindraProduct.PurchasePrice, SellingPrice = mahindraProduct.SellingPrice };
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
