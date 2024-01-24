using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IYamahaService
    {
        public Task<bool> AddProduct(YamahaProduct yamahaProduct);
        public Task<bool> UpdateProduct(YamahaProduct yamahaProduct);
        public Task<YamahaProduct> GetProduct(int id);
        public Task<List<YamahaProduct>> GetProductsList();
        public Task<bool> DeleteProduct(int id);
        public Task<bool> IsExistsProduct(int id);
    }
}
