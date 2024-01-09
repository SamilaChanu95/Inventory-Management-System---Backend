using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IBajajService
    {
        public Task<bool> AddBajajProduct(BajajProduct bajajProduct);
        public Task<bool> IsExistProduct(int id);
        public Task<bool> UpdateBajajProduct(BajajProduct bajajProduct);
        public Task<bool> DeleteBajajProduct(int id);
        public Task<BajajProduct> GetBajajProduct(int id);
        public Task<List<BajajProduct>> GetBajajProductsList();
    }
}
