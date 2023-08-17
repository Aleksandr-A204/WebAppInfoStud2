using System.Runtime.Serialization;

namespace WebAppInfoStud2.Models;

[DataContract]
public partial class StreetTable
{
    [DataMember(Name ="id")]
    public long Id { get; set; }

    [DataMember(Name = "street")]
    public string Street { get; set; } = null!;

    [DataMember(Name = "addresses")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
