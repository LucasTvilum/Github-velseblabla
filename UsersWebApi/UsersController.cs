using Microsoft.AspNetCore.Mvc;
using UsersWebApi.Models;
using UsersWebApi.Repositories;

namespace UsersWebApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository)
    {
        _repository = repository;
    }

    // SPRINT 1 - feature/register-user
    // POST /users/register
    [HttpPost("register")]
    public IActionResult Register([FromBody] User? newUser)
    {
        if (newUser == null || string.IsNullOrWhiteSpace(newUser.Username) || string.IsNullOrWhiteSpace(newUser.Password))
            return BadRequest("Username og password er påkrævet.");

        if (_repository.GetByUsername(newUser.Username) != null)
            return Conflict("Brugernavn findes allerede.");

        var created = _repository.Add(newUser);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // SPRINT 1 - feature/login-auth
    // POST /users/login
    [HttpPost("login")]
    public IActionResult Login([FromBody] User? loginRequest)
    {
        if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
            return BadRequest("Username og password er påkrævet.");

        var user = _repository.GetByUsername(loginRequest.Username);

        if (user == null || user.Password != loginRequest.Password)
            return Unauthorized("Forkert brugernavn eller password.");

        return Ok($"Velkommen, {user.Username}! Login lykkedes.");
    }

    // SPRINT 2 - feature/get-user-by-id
    // GET /users/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _repository.GetById(id);
        return user == null ? NotFound() : Ok(user);
    }

    // SPRINT 2 - feature/get-users
    // GET /users
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAll());
    }
}
