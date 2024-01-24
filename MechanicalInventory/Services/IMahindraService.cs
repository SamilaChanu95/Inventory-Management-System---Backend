using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IMahindraService
    {
        public Task<bool> AddProduct(MahindraProduct mahindraProduct);
        public Task<bool> UpdateProduct(MahindraProduct mahindraProduct);
        public Task<MahindraProduct> GetProduct(int id);
        public Task<List<MahindraProduct>> GetProductsList();
        public Task<bool> DeleteProduct(int id);
        public Task<bool> IsExistsProduct(int id);
    }
}
