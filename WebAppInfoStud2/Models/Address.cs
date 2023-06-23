namespace WebAppInfoStud2.Models;

public partial class Address
{
    public int Id { get; set; }
    public string? City { get; set; }
    public string? PostIndex { get; set; }
    public string? Street { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
}
