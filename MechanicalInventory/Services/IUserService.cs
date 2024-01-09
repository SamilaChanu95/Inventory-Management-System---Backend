using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public interface IUserService
    {
        public Task<User> GetUserDetails(int id);
        public Task<User> GetUserByUsernamePassowrd(UserCredential userCredential);
    }
}
