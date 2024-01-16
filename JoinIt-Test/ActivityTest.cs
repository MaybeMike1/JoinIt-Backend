using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoinIt_Backend.Features.Activity.Services;
using JoinIt_Backend.Features.Authentication.Services;
using JoinIt_Backend.Shared.Data;
using JoinIt_Backend.Shared.Models;
using JoinIt_Test.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

public class ActivityServiceTests
{
    private Mock<IActivityService> _activityServiceMock;
    private Mock<DatabaseContext> _databaseContextMock;
    private Mock<ILogger<ActivityService>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _activityServiceMock = new Mock<IActivityService>();
        _databaseContextMock = new Mock<DatabaseContext>();
        _loggerMock = new Mock<ILogger<ActivityService>>();

    }

    [Test]
    public async Task Assert_Ok_On_Activities()
    {
        var fakeUserList = TestHelper.GetFakeUserList(10);
        var fakeActivityList = TestHelper.GetFakeActivities(10, fakeUserList);
        var expectedStatusCode = 200;
        IActivityService activityService = new ActivityService(_databaseContextMock.Object, _loggerMock.Object);

        _databaseContextMock.Setup(x => x.Activities)
                .ReturnsDbSet(fakeActivityList);

        _databaseContextMock.Setup(x => x.Attendances)
            .ReturnsDbSet(new List<Attendance>());
        //TODO - Implement Attendance List

        var result = await activityService.GetActivities();

        Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
    }

}
