using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebAppInfoStud2.Models;

[DataContract]
public partial class FacultyTable
{
    [DataMember(Name ="id")]
    public long Id { get; set; }

    [DataMember(Name = "faculty")]
    public string Faculty { get; set; } = null!;

    [DataMember(Name = "curriculums")]
    public virtual ICollection<Curriculum> Curriculums { get; set; } = new List<Curriculum>();
}
