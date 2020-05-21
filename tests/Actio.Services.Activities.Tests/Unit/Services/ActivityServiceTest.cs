using System;
using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;
using Action.Services.Activities.Services;
using Moq;
using Xunit;

namespace Actio.Services.Activities.Tests.Unit.Services
{
    public class ActivityServiceTest
    {
        private Mock<IActivityRepository> _activityRepositoryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;

        public ActivityServiceTest()
        {
            _activityRepositoryMock = new Mock<IActivityRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
        }

        [Fact]
        public async void CreateActivity_ShouldCreateActivity()
        {

        //arrange
        var category = "Test";
        _categoryRepositoryMock.Setup(x => x.GetAsync(category)).ReturnsAsync(new Category(category));
        var activityService = new ActivityService(_activityRepositoryMock.Object, _categoryRepositoryMock.Object);

        //act
        
        await activityService.AddAsync(Guid.NewGuid(), Guid.NewGuid(), category, "activity", "description", DateTime.Now);
        //assert

        _categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.Once);
        _activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);

        }
        
    }
}