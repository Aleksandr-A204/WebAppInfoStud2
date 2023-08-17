using System.Runtime.Serialization;

namespace WebAppInfoStud2.Models;

[DataContract]
public partial class GroupEntity
{
    [DataMember(Name = "id")]
    public long Id { get; set; }

    [DataMember(Name = "group")]
    public string Group { get; set; } = null!;
}
