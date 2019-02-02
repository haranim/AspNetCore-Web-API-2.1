using Casino;
using Casino.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class IntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
      
        public IntegrationTests()
        {
            // Arrange
            //ConfigurationManager.AppSettings["Secret"] = "1234";
            var _server1 = new TestServer(new WebHostBuilder()
                                     .UseStartup<Startup>().UseSetting("AppSettings", JsonConvert.SerializeObject(new { Secret = 1234 })));


            //_server = new TestServer(new WebHostBuilder()
            //                         .UseStartup<Startup>().ConfigureAppConfiguration((context, config) =>
            //                         {
            //                             config.SetBasePath(
            //                                Path.Combine(
            //            Directory.GetCurrentDirectory(),
            //            "..\\..\\..\\")
            //                                );

            //                             config.AddJsonFile("appsettings.json");
            //                         }));

            _client = _server.CreateClient();
            //_configuration = new Mock<IConfiguration>();
            //_configuration.Setup(m => m.GetSection("AppSettings")).Returns(new KeyValuePair("xcv","xcv"));


        }

        [Fact]
        public async Task Persons_Get_All()
        {

            

            // Act
            var response = await _client.GetAsync("/casino/users");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(responseString);
            users.Count().Should().Be(10);
        }

        [Fact]
        public async Task Persons_Get_Specific()
        {
            // Act
            var response = await _client.GetAsync("/casino/users/16");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var user = JsonConvert.DeserializeObject<User>(responseString);
            user.Id.Should().Be(new ObjectId("16"));
        }

        [Fact]
        public async Task Persons_Register_User()
        {
            // Arrange
            var personToAdd = new User
            {
                Username = "John",
                Password = "Doe"
            };
            var content = JsonConvert.SerializeObject(personToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/casino/users/register", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<User>(responseString);
            person.Username.Should().Be("John");
        }

        [Fact]
        public async Task Persons_Login_User()
        {
            // Arrange
            var personToAdd = new User
            {
                Username = "Test1",
                Password = "Test1"
            };
            var content = JsonConvert.SerializeObject(personToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/casino/users/login", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
