using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface ISuzukiService
    {
        public Task<bool> AddProduct(SuzukiProduct suzukiProduct);
        public Task<bool> UpdateProduct(SuzukiProduct suzukiProduct);
        public Task<SuzukiProduct> GetProduct(int id);
        public Task<List<SuzukiProduct>> GetProductsList();
        public Task<bool> DeleteProduct(int id);
        public Task<bool> IsExistsProduct(int id);
    }
}
