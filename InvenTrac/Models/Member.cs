namespace InvenTrac.Models;

public class Member
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int GroupRoleId { get; set; }
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public int MemberRoleId { get; set; }
    public MemberRole? MemberRole { get; set; }
}
