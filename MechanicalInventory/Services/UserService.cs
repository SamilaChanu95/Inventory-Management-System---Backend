using Dapper;
using MechanicalInventory.Context;
using MechanicalInventory.Models;

namespace MechanicalInventory.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetUserDetails(int id)
        {
            var query = $"SELECT * FROM [user] WHERE id = {id};";
            using (var connection = _dataContext.CreateDbConnection()) 
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query);
                return user;
            }
        }

        public async Task<User> GetUserByUsernamePassowrd(UserCredential userCredential) 
        {
            var query = $"SELECT * FROM [user] WHERE username = '{userCredential.Username}' and password = '{userCredential.Password}' and role = '{userCredential.Role}';";
            using (var connection = _dataContext.CreateDbConnection()) 
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query);
                return user;
            }
        }
    }
}
