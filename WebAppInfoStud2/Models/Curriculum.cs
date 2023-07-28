using System;
using System.Collections.Generic;

namespace WebAppInfoStud2.Models;

public partial class Curriculum
{
    public long Id { get; set; }

    public long? FacultyId { get; set; }

    public long? SpecialityId { get; set; }

    public long? CourseId { get; set; }

    public long? GroupId { get; set; }

    public virtual CourseTable? Course { get; set; }

    public virtual FacultyTable? Faculty { get; set; }

    public virtual GroupTable? Group { get; set; }

    public virtual SpecialityTable? Speciality { get; set; }
}
