using System;
using System.Net;
using System.Security.Claims;
using Action.Api.Controllers;
using Action.Api.Services;
using Action.Common.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using Xunit;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class ActivityControllerTest
    {
        private Mock<IBusClient> _busClientMock;
        private Mock<IActivityService> _activityServiceMock;

        public ActivityControllerTest()
        {
            _busClientMock = new Mock<IBusClient>();
            _activityServiceMock = new Mock<IActivityService>();
        }

        [Fact]
        public async void ActivityControllerPost_ShouldReturnAccepted()
        {
            //arrange
            var activityController = new ActivityController(_busClientMock.Object, _activityServiceMock.Object);
            
            var userId = Guid.NewGuid();
            activityController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new System.Security.Claims.ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim []
                            {
                                new Claim(ClaimTypes.Name, userId.ToString())
                            },
                            "test")
                            )
                }
            };
            

            var activityCommand = new CreateActivityCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Category = "TestCategory",
                UserId = userId

            };
            
            //act

            var result = await activityController.CreateActivity(activityCommand);
            var resultContent = result as AcceptedResult;

            //assert

            resultContent.Should().NotBeNull();
            resultContent.StatusCode.Should().Be((int)HttpStatusCode.Accepted);
            resultContent.Location.Should().BeEquivalentTo($"activities/{activityCommand.Id}");
        }
    }
}