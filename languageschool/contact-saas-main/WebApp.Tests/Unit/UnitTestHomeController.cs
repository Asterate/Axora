using System;
using System.Threading.Tasks;
using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using WebApp.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace WebApp.Tests.Unit;

public class UnitTestHomeController
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HomeController _homeController;
    private readonly AppDbContext _ctx;

    public UnitTestHomeController(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        // set up logger - it is not mocked, so we are not testing logging functionality
        using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = logFactory.CreateLogger<HomeController>();

        // Create mock UserManager
        var store = new Mock<IUserStore<AppUser>>();
        var userManager = new Mock<UserManager<AppUser>>(
            store.Object, default!, default!, default!, default!, default!, default!, default!, default!);

        //set up controller
        _homeController = new HomeController(_ctx, logger, userManager.Object);
    }
    
    [Fact]
    public async Task IndexAction_ReturnsNullVm()
    {
        var result = (await _homeController.Index()) as ViewResult;
        _testOutputHelper.WriteLine(result?.ToString());
        var vm = result?.Model; // as HomeIndexViewModel;
        Assert.Null(vm);
    }

}