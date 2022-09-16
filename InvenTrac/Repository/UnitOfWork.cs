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
    public IGroupRepository Groups => new GroupRepository(_context);
    public IItemEntryRepository ItemEntries => new ItemEntryRepository(_context);
    public IMemberRepository Members => new MemberRepository(_context);
    public IMemberRoleRepository MemberRoles => new MemberRoleRepository(_context);
    public IWorkspaceRepository Workspaces => new WorkspaceRepository(_context);
    #endregion

    public async Task<bool> SaveAsync()
    {
        var saved = await _context.SaveChangesAsync(); // Returns a number
        return saved > 0 ? true : false;
    }
}
