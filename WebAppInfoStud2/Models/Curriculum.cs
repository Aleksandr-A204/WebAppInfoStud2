using System.Runtime.Serialization;

namespace WebAppInfoStud2.Models;

[DataContract]
public partial class Curriculum
{
    [DataMember(Name ="id")]
    public long Id { get; set; }

    [DataMember(Name = "facultyId")]
    public long FacultyId { get; set; }

    [DataMember(Name = "specialityId")]
    public long SpecialityId { get; set; }

    [DataMember(Name = "course")]
    public string Course { get; set; } = null!;

    [DataMember(Name = "group")]
    public string Group { get; set; } = null!;

    [DataMember(Name = "faculty")]
    public virtual FacultyTable? Faculty { get; set; }

    [DataMember(Name = "speciality")]
    public virtual SpecialityTable? Speciality { get; set; }
}
