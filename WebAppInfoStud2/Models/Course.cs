using System.Runtime.Serialization;

namespace WebAppInfoStud2.Models;

[DataContract]
public partial class CourseEntity
{
    [DataMember(Name = "id")]
    public long Id { get; set; }

    [DataMember(Name = "course")]
    public string Course { get; set; } = null!;
}