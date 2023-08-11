using System;
using System.Collections.Generic;

namespace WebAppInfoStud2.Models;

public partial class Student
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Postindex { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Faculty { get; set; } = null!;

    public string Speciality { get; set; } = null!;

    public string Course { get; set; } = null!;

    public string Group { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;
}
