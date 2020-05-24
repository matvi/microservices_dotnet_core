using System;
using Action.Common.Auth;
using Action.Services.Identity.Domain.Models;
using Action.Services.Identity.Domain.Repositories;
using Action.Services.Identity.Domain.Services;
using Action.Services.Identity.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Actio.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IEncrypter> _encrypterMock;
        private readonly Mock<IJwtHandler> _jwtHandlerMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTest()
        {
            _encrypterMock = new Mock<IEncrypter>();
            _jwtHandlerMock = new Mock<IJwtHandler>();
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async void UserServiceLogin_ShouldReturnJwt()
        {
            //arange
            var email = "test@gmail.com";
            var name = "testUserName";
            var salt = "salt";
            var password = "password";
            var token = "ejwt";
            var hash = "hash";

            _encrypterMock.Setup(x => x.GetSalt()).Returns(salt);
            _encrypterMock.Setup(x => x.GetHash(password, salt)).Returns(hash);
            _jwtHandlerMock.Setup(s => s.GenerateToken(It.IsAny<Guid>())).Returns(new JsonWebToken(token, DateTime.Now.AddMinutes(5)));

            var user = new User(email, name);
            user.SetPassword(password, _encrypterMock.Object);
            var userResult = _userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);

            //act
            
            var userService = new UserService(_userRepositoryMock.Object, _encrypterMock.Object, _jwtHandlerMock.Object);
            var jwt = await userService.LoginAsync(email, password);

            //assert

            jwt.Should().NotBeNull();
            jwt.Token.Should().BeEquivalentTo(token);
        }
        
    }
}