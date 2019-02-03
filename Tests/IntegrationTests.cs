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
            _server = new TestServer(new WebHostBuilder()
                            .UseEnvironment("Development")
                            .UseContentRoot("C:\\work\\MyProjects\\New folder\\Casino\\WebAPI\\Tests")
                            .UseConfiguration(new ConfigurationBuilder()
                                .SetBasePath("C:\\work\\MyProjects\\New folder\\Casino\\WebAPI\\Tests")
                                .AddJsonFile("appsettings.json")
                                .Build()
                            )
                            .UseStartup<Startup>());

            _client = _server.CreateClient();

        }

        [Fact]
        public async Task Users_Get_All()
        {
            var response = await _client.GetAsync("/casino/users");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Users_Register_User()
        {
            var personToAdd = new
            {
                Username = "John",
                Password = "Doe"
            };
            var content = JsonConvert.SerializeObject(personToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/casino/users/register", stringContent);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Users_Login_User()
        {
            var personToAdd = new
            {
                Username = "Test",
                Password = "Test"
            };
            var content = JsonConvert.SerializeObject(personToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/casino/users/login", stringContent);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Games_Get_All()
        {
            var response = await _client.GetAsync("/casino/games");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GamesCollection_Get_All()
        {
            var response = await _client.GetAsync("/casino/games/collection");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
