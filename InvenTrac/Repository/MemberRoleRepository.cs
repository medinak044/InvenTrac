using InvenTrac.Data;
using InvenTrac.Models;
using InvenTrac.Repository.IRepository;

namespace InvenTrac.Repository;

public class MemberRoleRepository : Repository<MemberRole>, IMemberRoleRepository
{
    private readonly AppDbContext _context;

    public MemberRoleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
