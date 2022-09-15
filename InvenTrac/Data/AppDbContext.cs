using InvenTrac.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvenTrac.Data;

public class AppDbContext: IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

    #region Tables
    public DbSet<Group> Groups { get; set; }
    public DbSet<ItemEntry> ItemEntries { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<MemberRole> MemberRoles { get; set; }
    public DbSet<Workspace> Workspaces { get; set; }
    #endregion
}
