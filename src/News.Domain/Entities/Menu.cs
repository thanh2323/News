namespace News.Domain.Entities;

public class Menu
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation Property
    public ICollection<NewsList> NewsList { get; set; } = new List<NewsList>();
}