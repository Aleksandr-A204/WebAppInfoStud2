using System;
using System.Collections.Generic;

namespace WebAppInfoStud2.Models;

public partial class CourseTable
{
    public long Id { get; set; }

    public string Course { get; set; } = null!;

    public virtual ICollection<Curriculum> Curriculums { get; set; } = new List<Curriculum>();
}
