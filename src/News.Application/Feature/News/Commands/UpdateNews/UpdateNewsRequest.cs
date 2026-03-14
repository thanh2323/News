namespace News.Application.Feature.News.Commands.UpdateNews;

public record UpdateNewsRequest(string Title, string Content, string? ImageUrl);