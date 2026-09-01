using UsersWebApi.Models;

namespace UsersWebApi.Repositories;

// Simpel in-memory "database" - god til at lære uden at skulle sætte en rigtig DB op
public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public List<User> GetAll() => _users;

    public User? GetById(int id) =>
        _users.FirstOrDefault(u => u.Id == id);

    public User? GetByUsername(string username) =>
        _users.FirstOrDefault(u => u.Username == username);

    public User Add(User user)
    {
        user.Id = _nextId++;
        _users.Add(user);
        return user;
    }
}
