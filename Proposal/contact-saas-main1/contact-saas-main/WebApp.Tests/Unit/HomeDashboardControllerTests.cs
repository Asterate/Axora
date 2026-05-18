using System.Security.Claims;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Shared.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.ViewModels;

namespace WebApp.Tests.Unit;

public class HomeDashboardControllerTests
{
    private readonly Mock<IProjectService> _projectService;
    private readonly Mock<IProjectTypeService> _projectTypeService;
    private readonly HomeDashboardController _controller;
    private readonly Guid _userId = Guid.NewGuid();

    public HomeDashboardControllerTests()
    {
        _projectService     = new Mock<IProjectService>();
        _projectTypeService = new Mock<IProjectTypeService>();
        _controller         = new HomeDashboardController(
            _projectService.Object,
            _projectTypeService.Object);

        var claims   = new[] { new Claim(ClaimTypes.NameIdentifier, _userId.ToString()) };
        var identity = new ClaimsIdentity(claims, "Test");
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(identity)
            }
        };
    }

    [Fact]
    public async Task Index_ReturnsViewWithProjects()
    {
        var fakeProjects = new List<ProjectListResponse> { new() { ProjectName = "My Project" } };
        _projectService.Setup(s => s.GetAllAsync(_userId))
            .ReturnsAsync(fakeProjects);

        var result = await _controller.Index();

        var view = Assert.IsType<ViewResult>(result);
        Assert.Equal(fakeProjects, view.Model);
    }

    [Fact]
    public async Task Create_Post_ValidModel_RedirectsToIndex()
    {
        var dto = new HomeDashboardViewModel
        {
            ProjectRequest = new SaveProjectRequest { ProjectNameEn = "New Project" }
        };

        var result = await _controller.Create(dto);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(_controller.Index), redirect.ActionName);
        _projectService.Verify(s => s.CreateAsync(dto.ProjectRequest, _userId), Times.Once);
    }

    [Fact]
    public async Task DeleteConfirmed_DeletesAndRedirectsToIndex()
    {
        var projectId = Guid.NewGuid();

        var result = await _controller.DeleteConfirmed(projectId);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(_controller.Index), redirect.ActionName);
        _projectService.Verify(s => s.DeleteAsync(projectId, _userId), Times.Once);
    }

    [Fact]
    public async Task Create_Post_InvalidModel_ReturnsViewWithProjectTypes()
    {
        _controller.ModelState.AddModelError("ProjectNameEn", "Required");
        _projectTypeService.Setup(s => s.GetActivesAsync()).ReturnsAsync(new List<LookupItem>());

        var result = await _controller.Create(new HomeDashboardViewModel());

        Assert.IsType<ViewResult>(result);
    }

    // NEW — IDOR: Details returns NotFound when project belongs to different institute
    [Fact]
    public async Task Details_WrongInstitute_ReturnsNotFound()
    {
        var projectId = Guid.NewGuid();

        // service returns null — IDOR check failed inside service
        _projectService
            .Setup(s => s.GetByIdAsync(projectId, _userId))
            .ReturnsAsync((ProjectResponse?)null);

        var result = await _controller.Details(projectId);

        Assert.IsType<NotFoundResult>(result);
    }
}