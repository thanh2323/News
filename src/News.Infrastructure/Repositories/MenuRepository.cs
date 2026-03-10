
using News.Domain.Entities;
using News.Infrastructure.Data;

namespace News.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly AppDbContext _context;

    public MenuRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Menu?> GetByIdAsync(int id)
    {
        return await _context.Menus.FindAsync(id);
    }

    public async Task AddAsync(Menu menu)
    {
         await _context.Menus.AddAsync(menu);
    }

    public void UpdateAsync(Menu menu)
    {
        _context.Menus.Update(menu);
    }

    public void Delete(Menu menu)
    {
        _context.Menus.Remove(menu);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}