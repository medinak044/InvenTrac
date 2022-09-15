namespace InvenTrac.Models;

public class Workspace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? OwnerId { get; set; }
    public AppUser? Owner { get; set; }
    public ICollection<Group>? Groups { get; set; }
}
