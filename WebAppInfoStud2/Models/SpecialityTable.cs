using System;
using System.Collections.Generic;

namespace WebAppInfoStud2.Models;

public partial class SpecialityTable
{
    public long Id { get; set; }

    public string Speciality { get; set; } = null!;

    public virtual ICollection<Curriculum> Curriculums { get; set; } = new List<Curriculum>();
}
