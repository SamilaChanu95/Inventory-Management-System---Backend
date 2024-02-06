using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class TvsService : ITvsService
    {
        private readonly DataContext _dataContext;
        public TvsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddTvsProduct(TvsProduct tvsProduct)
        {
            var sql = "INSERT INTO [tvsProduct]([tvsCode],[productName],[manufacturerName],[manufacturerYear],[quantity],[qualityLevel],[sellingPrice],[purchasePrice])" +
                " VALUES (@tvsCode, @productName, @manufacturerName, @manufacturerYear, @quantity, @qualityLevel, @sellingPrice, @purchasePrice);";
            /*var parameters = new DynamicParameters();
            parameters.Add("tvsCode", tvsProduct.TvsCode, DbType.String);
            parameters.Add("productName", tvsProduct.ProductName, DbType.String);
            parameters.Add("manufacturerName", tvsProduct.ManufacturerName, DbType.String);
            parameters.Add("manufacturerYear", tvsProduct.ManufacturerYear, DbType.DateTime);
            parameters.Add("quantity", tvsProduct.Quantity, DbType.Int32);
            parameters.Add("qualityLevel", tvsProduct.QualityLevel, DbType.Int32);
            parameters.Add("sellingPrice", tvsProduct.SellingPrice, DbType.Decimal);
            parameters.Add("purchasePrice", tvsProduct.PurchasePrice, DbType.Decimal);*/

            var product = new TvsProduct() { TvsCode = tvsProduct.ProductCode, ProductName = tvsProduct.ProductName, ManufacturerName = tvsProduct.ManufacturerName, ManufacturerYear = tvsProduct.ManufacturerYear, Quantity = tvsProduct.Quantity, QualityLevel = tvsProduct.QualityLevel, SellingPrice = tvsProduct.SellingPrice, PurchasePrice = tvsProduct.PurchasePrice };

            using (var connection = _dataContext.CreateDbConnection())
            {
                /*var result = await connection.ExecuteAsync(sql, parameters);*/
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }

        public async Task<bool> DeleteTvsProduct(int id)
        {
            var sql = $"DELETE FROM [tvsProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql);
                return (result > 0) ? true : false;
            }
        }

        public async Task<TvsProduct> GetTvsProduct(int id)
        {
            var sql = $"SELECT *, [tvsCode] AS productCode FROM [tvsProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<TvsProduct>(sql);
                return result;
            }
        }

        public async Task<List<TvsProduct>> GetTvsProductsList()
        {
            var sql = "SELECT *, [tvsCode] AS productCode FROM [tvsProduct] ORDER BY [id] DESC;";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryAsync<TvsProduct>(sql);
                return result.ToList();
            }
        }

        public async Task<bool> IsExistsProduct(int id)
        {
            var sql = $"SELECT [id] FROM [tvsProduct] WHERE [id] = '{id}';";
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql);
                return (result != null) ? true : false;
            }
        }

        public async Task<bool> UpdateTvsProduct(TvsProduct tvsProduct)
        {
            var sql = "UPDATE [tvsProduct] SET [tvsCode] = @tvsCode, [productName] = @productName, [manufacturerName] = @manufacturerName, [manufacturerYear] = @manufacturerYear, [quantity] = @quantity, [qualityLevel] = @qualityLevel, [sellingPrice] = @sellingPrice, [purchasePrice] = @purchasePrice WHERE [id] = @id;";
            var product = new TvsProduct() { Id = tvsProduct.Id, ProductName = tvsProduct.ProductName, TvsCode = tvsProduct.ProductCode, ManufacturerName = tvsProduct.ManufacturerName, ManufacturerYear = tvsProduct.ManufacturerYear, Quantity = tvsProduct.Quantity, QualityLevel = tvsProduct.QualityLevel, SellingPrice = tvsProduct.SellingPrice, PurchasePrice = tvsProduct.PurchasePrice };
            using (var connection = _dataContext.CreateDbConnection())
            {
                var result = await connection.ExecuteAsync(sql, product);
                return (result > 0) ? true : false;
            }
        }
    }
}
