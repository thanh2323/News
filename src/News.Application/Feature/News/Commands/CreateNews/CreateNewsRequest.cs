namespace News.Application.Feature.News.Commands.CreateNews;

public record CreateNewsRequest(string Title, string Content, string? ImageUrl, int MenuId);
