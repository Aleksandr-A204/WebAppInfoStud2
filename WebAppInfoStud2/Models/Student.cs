using System;
using System.Collections.Generic;

namespace WebAppInfoStud2.Models;

public partial class Student
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? City { get; set; }

    public string? Postindex { get; set; }

    public string? Street { get; set; }

    public string? Faculty { get; set; }

    public string? Speciality { get; set; }

    public string? Course { get; set; }

    public string? Group { get; set; }

    public long? ContactId { get; set; }

    public virtual Contact? Contact { get; set; }
}
