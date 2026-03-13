namespace News.Domain.Entities;

public class NewsList
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;

    public string? ImageUrl { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Foreign Key 
    public int MenuId { get; set; }

    // Navigation Property
    public Menu? Menu { get; set; }
}