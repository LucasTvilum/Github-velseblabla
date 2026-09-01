using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UsersWebApi.Controllers;
using UsersWebApi.Models;
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

    // ==========================================
    // SPRINT 1 - LOGIN TESTS (Par 2: Test 8 - 15)
    // ==========================================

    [TestMethod]
    public void Test08_Login_ValidCredentials_Returns200Ok()
    {
        // Arrange
        var loginRequest = new User { Username = "validuser", Password = "password123" };
        var storedUser = new User { Id = 1, Username = "validuser", Password = "password123" };
        _mockRepository.Setup(r => r.GetByUsername("validuser")).Returns(storedUser);

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult, "Expected OkObjectResult");
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [TestMethod]
    public void Test09_Login_RepositoryCalledWithCorrectData()
    {
        // Arrange
        var loginRequest = new User { Username = "validuser", Password = "password123" };
        var storedUser = new User { Id = 1, Username = "validuser", Password = "password123" };
        _mockRepository.Setup(r => r.GetByUsername("validuser")).Returns(storedUser);

        // Act
        _controller.Login(loginRequest);

        // Assert
        _mockRepository.Verify(r => r.GetByUsername("validuser"), Times.Once);
    }

    [TestMethod]
    public void Test10_Login_UserNotFound_Returns401Unauthorized()
    {
        // Arrange
        var loginRequest = new User { Username = "unknownuser", Password = "password123" };
        _mockRepository.Setup(r => r.GetByUsername("unknownuser")).Returns((User?)null);

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var unauthorizedResult = result as UnauthorizedObjectResult;
        Assert.IsNotNull(unauthorizedResult, "Expected UnauthorizedObjectResult");
        Assert.AreEqual(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
    }

    [TestMethod]
    public void Test10_Login_WrongPassword_Returns401Unauthorized()
    {
        // Arrange
        var loginRequest = new User { Username = "validuser", Password = "wrongpassword" };
        var storedUser = new User { Id = 1, Username = "validuser", Password = "correctpassword" };
        _mockRepository.Setup(r => r.GetByUsername("validuser")).Returns(storedUser);

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var unauthorizedResult = result as UnauthorizedObjectResult;
        Assert.IsNotNull(unauthorizedResult, "Expected UnauthorizedObjectResult");
        Assert.AreEqual(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
    }

    [TestMethod]
    public void Test11_Login_NullDto_Returns400BadRequest()
    {
        // Act
        var result = _controller.Login(null);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
        Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [TestMethod]
    public void Test12_Login_EmptyUsername_Returns400BadRequest()
    {
        // Arrange
        var loginRequest = new User { Username = "", Password = "password123" };

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
        Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [TestMethod]
    public void Test12_Login_WhitespaceUsername_Returns400BadRequest()
    {
        // Arrange
        var loginRequest = new User { Username = "   ", Password = "password123" };

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
        Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [TestMethod]
    public void Test13_Login_EmptyPassword_Returns400BadRequest()
    {
        // Arrange
        var loginRequest = new User { Username = "validuser", Password = "" };

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
        Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [TestMethod]
    public void Test13_Login_WhitespacePassword_Returns400BadRequest()
    {
        // Arrange
        var loginRequest = new User { Username = "validuser", Password = "   " };

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
        Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [TestMethod]
    public void Test14_Login_InvalidModelState_Returns400BadRequest()
    {
        // Arrange
        var loginRequest = new User { Username = "validuser", Password = "password123" };
        _controller.ModelState.AddModelError("Username", "Username is required");

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
        Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [TestMethod]
    public void Test15_Login_RepositoryThrowsException_Returns500InternalServerError()
    {
        // Arrange
        var loginRequest = new User { Username = "validuser", Password = "password123" };
        _mockRepository.Setup(r => r.GetByUsername("validuser")).Throws(new Exception("Database connection failure"));

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        var statusCodeResult = result as ObjectResult;
        Assert.IsNotNull(statusCodeResult, "Expected ObjectResult");
        Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }
}

