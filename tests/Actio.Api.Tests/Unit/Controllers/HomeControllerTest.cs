using Xunit;
using Microsoft.AspNetCore.Mvc;
using Action.Api.Controllers;
using FluentAssertions;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class HomeControllerTest
    {
        // "Hellow from Actino API"
        [Fact]
        public void HomeController_ShouldReturnStringContent()
        {
            //arrange
            var controller = new HomeController();
            //act
            var result = controller.GetAction();
            var contentResult = result as ContentResult;

            //assert
            
            contentResult.Should().NotBeNull();
            contentResult.Content.Should().BeEquivalentTo("Hello from Actino API");
            
        }
    }
}