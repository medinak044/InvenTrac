using InvenTrac.Data;
using InvenTrac.Models;
using InvenTrac.Repository.IRepository;

namespace InvenTrac.Repository;

public class GroupRepository : Repository<Group>, IGroupRepository
{
    private readonly AppDbContext _context;

    public GroupRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
