namespace InvenTrac.Repository.IRepository;

public interface IUnitOfWork
{
    Task<bool> SaveAsync();
    IGroupRepository Groups { get; }
    IItemEntryRepository ItemEntries { get; }
    IMemberRepository Members { get; }
    IMemberRoleRepository MemberRoles { get; }
    IWorkspaceRepository Workspaces { get; }
}
