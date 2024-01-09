using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;
using System.Data;

namespace MechanicalInventory.Services
{
    public class BajajService : IBajajService
    {
        private readonly DataContext _dataContext;
        public BajajService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddBajajProduct(BajajProduct bajajProduct)
        {
            var sql = "INSERT INTO [bajajProduct]([bajajCode],[productName],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice]) VALUES (@bajajCode, @productName, @manufacturerYear, @quantity, @qualityLevel, @sellingPrice, @purchasePrice);";
            var parameters = new DynamicParameters();
            parameters.Add("bajajCode", bajajProduct.BajajCode, DbType.String);
            parameters.Add("productName", bajajProduct.ProductName, DbType.String);
            parameters.Add("manufacturerYear", bajajProduct.ManufacturerYear, DbType.DateTime);
            parameters.Add("quantity", bajajProduct.Quantity, DbType.Int32);
            parameters.Add("qualityLevel", bajajProduct.QualityLevel, DbType.Int32);
            parameters.Add("sellingPrice", bajajProduct.SellingPrice, DbType.Decimal);
            parameters.Add("purchasePrice", bajajProduct.PurchasePrice, DbType.Decimal);

            using (var connection = _dataContext.CreateDbConnection()) 
            {
                var result = await connection.ExecuteAsync(sql, parameters);
                return (result > 0) ? true : false;   
            }
        }

        public async Task<bool> DeleteBajajProduct(int id)
        {
            var sql = $"DELETE FROM [bajajProduct] WHERE id = '{id}';";
            using (var connection = _dataContext.CreateDbConnection()) 
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0)? true : false; 
            }
        }

        public async Task<BajajProduct> GetBajajProduct(int id)
        {
            var sql = $"SELECT * FROM [bajajProduct] WHERE id = '{id}';";
            using (var connection = _dataContext.CreateDbConnection()) 
            {
                var bajajProduct = await connection.QueryFirstOrDefaultAsync<BajajProduct>(sql);
                return bajajProduct;
            }
        }

        public async Task<List<BajajProduct>> GetBajajProductsList()
        {
            var sql = "SELECT * FROM [bajajProduct] ORDER BY id DESC;";
            using (var connection = _dataContext.CreateDbConnection()) 
            {
                var productList = await connection.QueryAsync<BajajProduct>(sql); 
                return productList.ToList();
            }
        }

        public async Task<bool> IsExistProduct(int id)
        {
            var sql = $"SELECT id FROM [bajajProduct] WHERE id = '{id}';";
            using (var connection = _dataContext.CreateDbConnection()) 
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateBajajProduct(BajajProduct bajajProduct)
        {
            var sql = "UPDATE [bajajProduct] SET [bajajCode] = @bajajCode, [productName] = @productName, [manufacturerYear] = @manufacturerYear, " +
                "[quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            var parameters = new DynamicParameters();
            parameters.Add("id", bajajProduct.Id, DbType.Int32);
            parameters.Add("bajajCode", bajajProduct.BajajCode, DbType.String);
            parameters.Add("productName", bajajProduct.ProductName, DbType.String);
            parameters.Add("manufacturerYear", bajajProduct.ManufacturerYear, DbType.DateTime);
            parameters.Add("quantity", bajajProduct.Quantity, DbType.Int32);
            parameters.Add("qualityLevel", bajajProduct.QualityLevel, DbType.Int32);
            parameters.Add("sellingPrice", bajajProduct.SellingPrice, DbType.Decimal);
            parameters.Add("purchasePrice", bajajProduct.PurchasePrice, DbType.Decimal);

            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, parameters);
                return (result > 0) ? true : false;
            }
        }
    }
}
