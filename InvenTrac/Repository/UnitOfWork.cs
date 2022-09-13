using InvenTrac.Data;
using InvenTrac.Repository.IRepository;

namespace InvenTrac.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    #region Repository instances
    //public IItemRepository Items => new ItemRepository(_context);
    #endregion

    public async Task<bool> SaveAsync()
    {
        var saved = await _context.SaveChangesAsync(); // Returns a number
        return saved > 0 ? true : false;
    }
}
