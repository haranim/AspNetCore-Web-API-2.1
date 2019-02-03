using Casino.Controllers;
using Casino.IServices;
using Casino.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
            var controller = new UsersController(CreateMoqUsers().Object);

            var result = controller.Get();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var persons = okResult.Value.Should().BeAssignableTo<IEnumerable<User>>().Subject;

            persons.Count().Should().Be(3);
        }

        [Fact]
        public void GetById_UnknownId_ReturnsNotFoundResult()
        {
            var controller = new UsersController(CreateMoqUsers().Object);

            var result = controller.Get("1234");

            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {

            var user = new User()
            {
                Username = "John"
            };

            var controller = new UsersController(CreateMoqUsers().Object);
            controller.ModelState.AddModelError("Password", "Required");

            var result = controller.Register(user);

            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {

            var user = new User()
            {
                Username = "Alex",
                Password = "Password"
            };

            var controller = new UsersController(CreateMoqUsers().Object);

            var result = controller.Register(user) as CreatedAtActionResult;

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
                new User{Id=ObjectId.GenerateNewId(3), Username="Juergen", Password="Gutsch"}
              });

            return serviceMock;
        }

        private Mock<IGamesService> CreateMoqGames()
        {
            var serviceMock = new Mock<IGamesService>();
            serviceMock.Setup(x => x.GetGames(1,2)).Returns(() => new List<Games>
              {
                new Games{Id=ObjectId.GenerateNewId(4), Name ="Game1"},
                new Games{Id=ObjectId.GenerateNewId(5), Name="Game2"}
              });

            return serviceMock;
        }

        private Mock<ISessionsService> CreateMoqSession(Games game, User user)
        {
            var serviceMock = new Mock<ISessionsService>();
            serviceMock.Setup(x => x.CreateSession(game, user)).Returns(() => 
                new Session{ Id=ObjectId.GenerateNewId(6), UserId=ObjectId.GenerateNewId(1), GameId=ObjectId.GenerateNewId(4) });

            return serviceMock;
        }

        [Fact]
        public void GetGames_WhenCalled_ReturnsAllItems()
        {
            var controller = new GamesController(CreateMoqGames().Object);

            var result = controller.GetGames(1,2);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var games = okResult.Value.Should().BeAssignableTo<IEnumerable<Games>>().Subject;

            games.Count().Should().Be(2);
        }

        

    }
}
