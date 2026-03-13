using News.Domain.Entities;
using News.Infrastructure.Data;

namespace News.Infrastructure.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly AppDbContext _context;

    public NewsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NewsList?> GetByIdAsync(int id)
    {
        return await _context.News.FindAsync(id);
    }

    public async Task AddAsync(NewsList news)
    {
        await _context.News.AddAsync(news);
    }

    public void Update(NewsList news)
    {
        _context.News.Update(news);
    }

    public void Delete(NewsList news)
    {
        _context.News.Remove(news);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
