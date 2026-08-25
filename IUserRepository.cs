using UsersWebApi.Models;

namespace UsersWebApi.Repositories;

public interface IUserRepository
{
    List<User> GetAll();
    User? GetById(int id);
    User? GetByUsername(string username);
    User Add(User user);
}
