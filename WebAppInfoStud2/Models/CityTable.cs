using System.Runtime.Serialization;

namespace WebAppInfoStud2.Models;

[DataContract]
public partial class CityTable
{
    [DataMember(Name = "id")]
    public long Id { get; set; }

    [DataMember(Name = "city")]
    public string City { get; set; } = null!;

    [DataMember(Name = "addresses")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [DataMember(Name = "students")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
