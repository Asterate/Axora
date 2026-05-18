using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Services;
using App.Modules.Project.Domain;
using App.Shared.Contracts;
using App.Shared.Contracts.Events;
using App.Shared.Domain;
using App.Shared.Helpers;
using MediatR;
using Moq;

namespace WebApp.Tests.Unit;

public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _repo;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly Mock<IMediator> _mediator;
    private readonly ProjectService _service;

    public ProjectServiceTests()
    {
        _repo     = new Mock<IProjectRepository>();
        _uow      = new Mock<IUnitOfWork>();
        _mediator = new Mock<IMediator>();
        _service  = new ProjectService(_repo.Object, _uow.Object, _mediator.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOnlyProjectsForUsersInstitute()
    {
        var userId      = Guid.NewGuid();
        var instituteId = Guid.NewGuid();
        var otherId     = Guid.NewGuid();

        SetupMediator(userId, instituteId);

        _repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Project>
        {
            MakeProject(instituteId),
            MakeProject(otherId)
        });

        var result = (await _service.GetAllAsync(userId)).ToList();

        Assert.Single(result);
    }

    [Fact]
    public async Task CreateAsync_LinksInstituteAndSaves()
    {
        var userId      = Guid.NewGuid();
        var instituteId = Guid.NewGuid();
        var request     = new SaveProjectRequest { ProjectNameEn = "Test Project" };

        SetupMediator(userId, instituteId);

        Project? savedEntity = null;
        _repo
            .Setup(r => r.AddAsync(It.IsAny<Project>()))
            .Callback<Project>(e => savedEntity = e)
            .Returns(Task.CompletedTask);

        _repo
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() =>
            {
                if (savedEntity == null) return null;
                savedEntity.ProjectType = new ProjectType
                {
                    Name = new LangStr { [Cultures.English] = "Type", [Cultures.Estonian] = "Type" }
                };
                return savedEntity;
            });

        await _service.CreateAsync(request, userId);

        Assert.Single(savedEntity!.InstituteProjects);
        Assert.Equal(instituteId, savedEntity.InstituteProjects.First().InstituteId);
        Assert.NotEqual(default, savedEntity.CreatedAt);
        _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_SoftDeletesSetsDeletedAt()
    {
        var userId    = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var instituteId = Guid.NewGuid();

        SetupMediator(userId, instituteId);

        var entity = MakeProject(instituteId);
        entity.Id = projectId;

        _repo.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(entity);

        await _service.DeleteAsync(projectId, userId);

        Assert.NotNull(entity.DeletedAt);
        _repo.Verify(r => r.Update(entity), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    // NEW — IDOR: user from different institute cannot delete another institute's project
    [Fact]
    public async Task DeleteAsync_WrongInstitute_DoesNotDeleteOrSave()
    {
        var userId      = Guid.NewGuid();
        var instituteId = Guid.NewGuid();
        var otherId     = Guid.NewGuid();
        var projectId   = Guid.NewGuid();

        SetupMediator(userId, otherId); // user belongs to otherId

        var entity = MakeProject(instituteId); // project belongs to instituteId
        entity.Id = projectId;

        _repo.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(entity);

        await _service.DeleteAsync(projectId, userId);

        Assert.Null(entity.DeletedAt);                              // not soft deleted
        _repo.Verify(r => r.Update(It.IsAny<Project>()), Times.Never); // Update never called
        _uow.Verify(u => u.SaveChangesAsync(), Times.Never);            // nothing saved
    }

    // NEW — IDOR: GetByIdAsync returns null for wrong institute
    [Fact]
    public async Task GetByIdAsync_WrongInstitute_ReturnsNull()
    {
        var userId      = Guid.NewGuid();
        var instituteId = Guid.NewGuid();
        var otherId     = Guid.NewGuid();
        var projectId   = Guid.NewGuid();

        SetupMediator(userId, otherId); // user belongs to otherId

        var entity = MakeProject(instituteId); // project belongs to instituteId
        entity.Id = projectId;

        _repo.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(projectId, userId);

        Assert.Null(result); // treated as not found — IDOR protection
    }

    // shared mediator setup
    private void SetupMediator(Guid userId, Guid instituteId) =>
        _mediator
            .Setup(m => m.Send(
                It.Is<InstituteUserEvent.GetInstituteIdByUserIdQuery>(q => q.UserId == userId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(instituteId);

    private static Project MakeProject(Guid instituteId) => new()
    {
        Id = Guid.NewGuid(),
        ProjectName = new LangStr { [Cultures.English] = "Test", [Cultures.Estonian] = "Test" },
        ProjectType = new ProjectType
        {
            Name = new LangStr { [Cultures.English] = "Type", [Cultures.Estonian] = "Type" }
        },
        InstituteProjects = new List<InstituteProject>
        {
            new() { InstituteId = instituteId }
        }
    };
}