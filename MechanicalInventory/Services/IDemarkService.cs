using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IDemarkService
    {
        public Task<bool> AddProduct(DemarkProduct demarkProduct);
        public Task<bool> UpdateProduct(DemarkProduct demarkProduct);
        public Task<DemarkProduct> GetProduct(int id);
        public Task<List<DemarkProduct>> GetProductsList();
        public Task<bool> DeleteProduct(int id);
        public Task<bool> IsExistsProduct(int id);
    }
}
