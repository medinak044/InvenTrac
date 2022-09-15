namespace InvenTrac.Models;

public class ItemEntry
{
    public int Id { get; set; }
    public int Quantity { get; set; } = 1;
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool HasExpirationDate { get; set; } = false;
    public DateTime? ExpirationDate { get; set; }
}
