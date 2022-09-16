using InvenTrac.Data;
using InvenTrac.Models;
using InvenTrac.Repository.IRepository;

namespace InvenTrac.Repository;

public class MemberRepository : Repository<Member>, IMemberRepository
{
    private readonly AppDbContext _context;

    public MemberRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
