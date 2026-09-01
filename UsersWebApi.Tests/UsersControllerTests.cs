using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsersWebApi.Controllers;
using UsersWebApi.Repositories;

namespace UsersWebApi.Tests;

[TestClass]
public class UsersControllerTests
{
    private Mock<IUserRepository> _mockRepository = null!;
    private UsersController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IUserRepository>();

        _controller = new UsersController(_mockRepository.Object);
    }
}
