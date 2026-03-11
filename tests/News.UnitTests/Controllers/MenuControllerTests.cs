using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using News.API.Controller;
using News.Application.DTOs.Menu;
using News.Application.Feature.Menus.Commands.CreateMenu;
using News.Application.Feature.Menus.Commands.DeleteMenu;
using News.Application.Feature.Menus.Commands.UpdateMenu;
using News.Application.Feature.Menus.Queries.GetMenuById;
using News.Application.Feature.Menus.Queries.GetMenus;

namespace News.UnitTests.Controllers;

public class MenuControllerTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly MenuController _controller;

    public MenuControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _controller = new MenuController(_senderMock.Object);
    }

    // ─── GetMenus ────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetMenus_ReturnsOkWithListOfMenus()
    {
        // Arrange
        var menus = new List<MenuDto>
        {
            new MenuDto { Id = 1, Name = "Home" },
            new MenuDto { Id = 2, Name = "About" }
        };

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetMenusQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(menus);

        // Act
        var result = await _controller.GetMenus();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(menus, okResult.Value);
    }

    [Fact]
    public async Task GetMenus_ReturnsOkWithEmptyList_WhenNoMenusExist()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(It.IsAny<GetMenusQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<MenuDto>());

        // Act
        var result = await _controller.GetMenus();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Empty((IEnumerable<MenuDto>)okResult.Value!);
    }

    // ─── GetMenuById ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetMenu_ReturnsOkWithMenu_WhenMenuExists()
    {
        // Arrange
        var menu = new MenuDto { Id = 1, Name = "Home" };

        _senderMock
            .Setup(s => s.Send(It.Is<GetMenuByIdQuery>(q => q.Id == 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(menu);

        // Act
        var result = await _controller.GetMenu(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(menu, okResult.Value);
    }

    [Fact]
    public async Task GetMenu_ReturnsNotFound_WhenMenuDoesNotExist()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(It.Is<GetMenuByIdQuery>(q => q.Id == 99), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MenuDto?)null);

        // Act
        var result = await _controller.GetMenu(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    // ─── Create ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WithNewId()
    {
        // Arrange
        var command = new CreateMenuCommand("New Menu");

        _senderMock
            .Setup(s => s.Send(It.Is<CreateMenuCommand>(c => c.Name == command.Name), It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);

        // Act
        var result = await _controller.Create(command);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetMenu), createdResult.ActionName);
        Assert.Equal(5, createdResult.Value);
    }

    [Fact]
    public async Task Create_SendsCorrectCommand()
    {
        // Arrange
        var command = new CreateMenuCommand("Test Menu");

        _senderMock
            .Setup(s => s.Send(It.IsAny<CreateMenuCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _controller.Create(command);

        // Assert
        _senderMock.Verify(
            s => s.Send(It.Is<CreateMenuCommand>(c => c.Name == "Test Menu"), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // ─── Update ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task Update_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var request = new UpdateMenuRequest("Updated Name");

        _senderMock
            .Setup(s => s.Send(It.IsAny<UpdateMenuCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Unit.Value));

        // Act
        var result = await _controller.Update(1, request);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_SendsCorrectCommandWithIdAndName()
    {
        // Arrange
        var request = new UpdateMenuRequest("Updated Name");

        _senderMock
            .Setup(s => s.Send(It.IsAny<UpdateMenuCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Unit.Value));

        // Act
        await _controller.Update(3, request);

        // Assert
        _senderMock.Verify(
            s => s.Send(
                It.Is<UpdateMenuCommand>(c => c.Id == 3 && c.Name == "Updated Name"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // ─── Delete ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(It.IsAny<DeleteMenuCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Unit.Value));

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_SendsCorrectCommandWithId()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(It.IsAny<DeleteMenuCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Unit.Value));

        // Act
        await _controller.Delete(7);

        // Assert
        _senderMock.Verify(
            s => s.Send(
                It.Is<DeleteMenuCommand>(c => c.Id == 7),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
