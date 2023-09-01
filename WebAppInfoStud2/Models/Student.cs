using System.Runtime.Serialization;

namespace WebAppInfoStud2.Models;

[DataContract]
public partial class Student
{

    [DataMember(Name = "id")]
    public long Id { get; set; }

    [DataMember(Name = "fullName")]
    public string FullName { get; set; } = null!;

    [DataMember(Name = "cityId")]
    public long CityId { get; set; }

    [DataMember(Name = "postindex")]
    public string Postindex { get; set; } = null!;

    [DataMember(Name = "street")]
    public string Street { get; set; } = null!;

    [DataMember(Name = "faculty")]
    public string Faculty { get; set; } = null!;

    [DataMember(Name = "speciality")]
    public string Speciality { get; set; } = null!;

    [DataMember(Name = "course")]
    public string Course { get; set; } = null!;

    [DataMember(Name = "group")]
    public string Group { get; set; } = null!;

    [DataMember(Name = "phone")]
    public string Phone { get; set; } = null!;

    [DataMember(Name = "email")]
    public string Email { get; set; } = null!;

    [DataMember(Name = "city")]
    public virtual CityTable? City { get; set; }
}
