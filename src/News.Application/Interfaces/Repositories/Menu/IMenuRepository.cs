using News.Domain.Entities;

public interface IMenuRepository
{
    Task<Menu?> GetByIdAsync(int id);

    Task AddAsync(Menu menu);

    void UpdateAsync(Menu menu);

    void Delete(Menu menu);

    Task SaveChangesAsync();
}