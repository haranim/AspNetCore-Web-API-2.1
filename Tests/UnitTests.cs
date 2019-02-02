using Casino.Controllers;
using Casino.IServices;
using Casino.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class UnitTests
    {
       
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            var controller = new UsersController(CreateMoqUsers().Object);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var persons = okResult.Value.Should().BeAssignableTo<IEnumerable<User>>().Subject;

            persons.Count().Should().Be(3);
        }

        [Fact]
        public void GetById_UnknownId_ReturnsNotFoundResult()
        {
            var controller = new UsersController(CreateMoqUsers().Object);
            // Act
            var result = controller.Get("1234");

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var user = new User()
            {
                Username = "John"
            };
            // Arrange
            var controller = new UsersController(CreateMoqUsers().Object);
            controller.ModelState.AddModelError("Password", "Required");
            // Act
            var result = controller.Register(user);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var user = new User()
            {
                Username = "Alex",
                Password = "Password"
            };

            // Act
            var controller = new UsersController(CreateMoqUsers().Object);

            // Act
            var result = controller.Register(user) as CreatedAtActionResult;
            

            // Assert
            var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var newUser = okResult.Value.Should().BeAssignableTo<User>().Subject;
            newUser.Username.Should().Be("Alex");
        }


        private Mock<IUserService> CreateMoqUsers()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(x => x.Get()).Returns(() => new List<User>
              {
                new User{Id=ObjectId.GenerateNewId(1), Username ="Steve", Password="Winter"},
                new User{Id=ObjectId.GenerateNewId(2), Username="John", Password="Doe"},
                new User{Id=ObjectId.GenerateNewId(3), Username="Juergen", Password="Gutsch"},
              });

            return serviceMock;
        }

    }
}
