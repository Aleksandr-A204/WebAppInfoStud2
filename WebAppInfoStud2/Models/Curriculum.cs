namespace WebAppInfoStud2.Models;

public partial class Curriculum
{
    public int Id { get; set; }
    public string? Faculty { get; set; }
    public string? Speciality { get; set; }
    public string? Course { get; set; }
    public string? Group { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
}
