using News.Domain.Entities;

public interface INewsRepository
{
    Task<NewsList?> GetByIdAsync(int id);
    Task AddAsync(NewsList news);
    void Update(NewsList news);
    void Delete(NewsList news);
    Task SaveChangesAsync();
}
