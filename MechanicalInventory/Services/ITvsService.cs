using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface ITvsService
    {
        public Task<bool> IsExistsProduct(int id);
        public Task<bool> AddTvsProduct(TvsProduct tvsProduct);
        public Task<bool> DeleteTvsProduct(int id);
        public Task<bool> UpdateTvsProduct(TvsProduct tvsProduct);
        public Task<TvsProduct> GetTvsProduct(int id);
        public Task<List<TvsProduct>> GetTvsProductsList();
    }
}
