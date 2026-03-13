using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using News.API.Controller;
using News.Application.DTOs.News;
using News.Application.Feature.News.Commands.CreateNews;
using News.Application.Feature.News.Commands.DeleteNews;
using News.Application.Feature.News.Commands.UpdateNews;
using News.Application.Feature.News.Queries.GetAllNews;
using News.Application.Feature.News.Queries.GetNewsById;

namespace News.UnitTests.Controllers;

public class NewsControllerTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly NewsController _controller;

    public NewsControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _controller = new NewsController(_senderMock.Object);
    }

    // ─── GetAllNews ────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllNews_ReturnsOkWithListOfNews()
    {
        var news = new List<NewsDto>
        {
            new NewsDto { Id = 1, Title = "News 1", Content = "Content 1", MenuId = 1 },
            new NewsDto { Id = 2, Title = "News 2", Content = "Content 2", MenuId = 1 }
        };

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetAllNewsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(news);

        var result = await _controller.GetAllNews(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(news, okResult.Value);
    }

    [Fact]
    public async Task GetAllNews_ReturnsOkWithEmptyList_WhenNoNewsExist()
    {
        _senderMock
            .Setup(s => s.Send(It.IsAny<GetAllNewsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<NewsDto>());

        var result = await _controller.GetAllNews(null);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Empty((IEnumerable<NewsDto>)okResult.Value!);
    }

    // ─── GetNewsById ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetNews_ReturnsOkWithNews_WhenNewsExists()
    {
        var news = new NewsDto { Id = 1, Title = "News 1", Content = "Content 1", MenuId = 1 };

        _senderMock
            .Setup(s => s.Send(It.Is<GetNewsByIdQuery>(q => q.Id == 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(news);

        var result = await _controller.GetNews(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(news, okResult.Value);
    }

    [Fact]
    public async Task GetNews_ReturnsNotFound_WhenNewsDoesNotExist()
    {
        _senderMock
            .Setup(s => s.Send(It.Is<GetNewsByIdQuery>(q => q.Id == 99), It.IsAny<CancellationToken>()))
            .ReturnsAsync((NewsDto?)null);

        var result = await _controller.GetNews(99);

        Assert.IsType<NotFoundResult>(result);
    }

    // ─── Create ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WithNewId()
    {
        var request = new CreateNewsRequest("Title", "Content", null, 1);

        _senderMock
            .Setup(s => s.Send(It.IsAny<CreateNewsCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);

        var result = await _controller.Create(request);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetNews), createdResult.ActionName);
        Assert.Equal(5, createdResult.Value);
    }

    [Fact]
    public async Task Create_SendsCorrectCommand()
    {
        var request = new CreateNewsRequest("Test Title", "Test Content", "image.jpg", 1);

        _senderMock
            .Setup(s => s.Send(It.IsAny<CreateNewsCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        await _controller.Create(request);

        _senderMock.Verify(
            s => s.Send(It.Is<CreateNewsCommand>(c => c.Title == "Test Title"), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // ─── Update ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task Update_ReturnsNoContent_WhenSuccessful()
    {
        var request = new UpdateNewsRequest("Updated Title", "Updated Content", "image.jpg");

        _senderMock
            .Setup(s => s.Send(It.IsAny<UpdateNewsCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(MediatR.Unit.Value));

        var result = await _controller.Update(1, request);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_SendsCorrectCommandWithIdAndData()
    {
        var request = new UpdateNewsRequest("New Title", "New Content", null);

        _senderMock
            .Setup(s => s.Send(It.IsAny<UpdateNewsCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(MediatR.Unit.Value));

        await _controller.Update(3, request);

        _senderMock.Verify(
            s => s.Send(
                It.Is<UpdateNewsCommand>(c => c.Id == 3 && c.Title == "New Title"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // ─── Delete ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenSuccessful()
    {
        _senderMock
            .Setup(s => s.Send(It.IsAny<DeleteNewsCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(MediatR.Unit.Value));

        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_SendsCorrectCommandWithId()
    {
        _senderMock
            .Setup(s => s.Send(It.IsAny<DeleteNewsCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(MediatR.Unit.Value));

        await _controller.Delete(7);

        _senderMock.Verify(
            s => s.Send(
                It.Is<DeleteNewsCommand>(c => c.Id == 7),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
