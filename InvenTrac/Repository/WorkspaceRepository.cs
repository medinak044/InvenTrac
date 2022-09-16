using InvenTrac.Data;
using InvenTrac.Models;
using InvenTrac.Repository.IRepository;

namespace InvenTrac.Repository;

public class WorkspaceRepository : Repository<Workspace>, IWorkspaceRepository
{
    private readonly AppDbContext _context;

    public WorkspaceRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
