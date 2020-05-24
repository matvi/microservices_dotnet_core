using System;
using System.Net.Http;
using System.Text;
using Action.Common.Auth;
using Action.Common.Commands;
using Action.Services.Identity;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Actio.Services.Identity.Tests.Integration
{
    public class AccountControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public AccountControllerTest()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();

        }

        [Fact]
        public async void Login_ShouldReturnJwtAndShouldPassExpirationTime()
        {

        //arrange
        var email = "david@gmail.com";
        var password = "123PAss";
        var authenticateUserCommand = new AuthenticateUser
        {
            Email = email,
            Password = password
        };
        
        var payload = JsonConvert.SerializeObject(authenticateUserCommand);
        HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

        //act
        var response = await _client.PostAsync("/Account/Login", httpContent);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<JsonWebToken>(content);

        //assert
        response.EnsureSuccessStatusCode();
        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrEmpty();
        result.Expires.Should().NotBeAfter(DateTime.Now.AddMinutes(6));
        }
        
    }
}