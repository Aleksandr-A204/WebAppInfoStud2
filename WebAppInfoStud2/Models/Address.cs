using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2;

[DataContract]
public partial class Address
{
    [DataMember(Name = "id")]
    public long Id { get; set; }

    [DataMember(Name = "cityId")]
    public long CityId { get; set; }

    [DataMember(Name = "postindexId")]
    public long PostindexId { get; set; }

    [DataMember(Name = "streetId")]
    public long StreetId { get; set; }

    [DataMember(Name = "city")]
    public virtual CityTable? City { get; set; }

    [DataMember(Name = "postindex")]
    public virtual PostindexTable? Postindex { get; set; }

    [DataMember(Name = "street")]
    public virtual StreetTable? Street { get; set; }
}