using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IHondaService
    {
        public Task<bool> AddProduct(HondaProduct hondaProduct);
        public Task<bool> DeleteProduct(int id);
        public Task<bool> UpdateProduct(HondaProduct hondaProduct);
        public Task<HondaProduct> GetProduct(int id);
        public Task<List<HondaProduct>> GetProductList();
        public Task<bool> IsExistsProduct(int id);
    }
}
