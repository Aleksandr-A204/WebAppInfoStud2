using System;
using System.Collections.Generic;

namespace WebAppInfoStud2.Models;

public partial class Contact
{
    public long Id { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
