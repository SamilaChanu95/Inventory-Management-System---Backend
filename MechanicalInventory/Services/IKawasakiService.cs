using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IKawasakiService
    {
        public Task<bool> AddProduct(KawasakiProduct kawasakiProduct);
        public Task<bool> UpdateProduct(KawasakiProduct kawasakiProduct);
        public Task<KawasakiProduct> GetProduct(int id);
        public Task<List<KawasakiProduct>> GetProductsList();
        public Task<bool> DeleteProduct(int id);
        public Task<bool> IsExistsProduct(int id);
    }
}
