namespace WebAppInfoStud2.Models;

public partial class Student
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public int AddressId { get; set; }
    public Address? Address { get; set; }
    public int CurriculumId { get; set; }
    public Curriculum? Curriculum { get; set; }
    public int ContactId { get; set; }
    public Contact? Contact { get; set; }
}
