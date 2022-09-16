using InvenTrac.Data;
using InvenTrac.Models;
using InvenTrac.Repository.IRepository;

namespace InvenTrac.Repository;

public class ItemEntryRepository : Repository<ItemEntry>, IItemEntryRepository
{
    private readonly AppDbContext _context;

    public ItemEntryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
