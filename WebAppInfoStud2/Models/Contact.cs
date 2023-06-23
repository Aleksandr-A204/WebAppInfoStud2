namespace WebAppInfoStud2.Models;

public partial class Contact
{
    public int Id { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Student? Student { get; set; }
}
