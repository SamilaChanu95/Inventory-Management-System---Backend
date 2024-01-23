using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IHeroService
    {
        public Task<bool> AddProduct(HeroProduct heroProduct);
        public Task<bool> DeleteProduct(int id);
        public Task<bool> UpdateProduct(HeroProduct heroProduct);
        public Task<HeroProduct> GetProduct(int id);
        public Task<List<HeroProduct>> GetProductList();
        public Task<bool> IsExistsProduct(int id);
    }
}
