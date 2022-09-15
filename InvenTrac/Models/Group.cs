namespace InvenTrac.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int WorkspaceId { get; set; }
    public Workspace? Workspace { get; set; }
    public ICollection<Member>? Members { get; set; }
    public ICollection<ItemEntry> ItemEntries { get; set; }
}
