namespace News.Application.DTOs.News;

public class NewsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public int MenuId { get; set; }
}
