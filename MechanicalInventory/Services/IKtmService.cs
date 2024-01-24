using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IKtmService
    {
        public Task<bool> AddProduct(KtmProduct ktmProduct);
        public Task<bool> UpdateProduct(KtmProduct ktmProduct);
        public Task<KtmProduct> GetProduct(int id);
        public Task<List<KtmProduct>> GetProductsList();
        public Task<bool> DeleteProduct(int id);
        public Task<bool> IsExistsProduct(int id);
    }
}
