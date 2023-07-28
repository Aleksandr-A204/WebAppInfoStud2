using System;
using System.Collections.Generic;

namespace WebAppInfoStud2.Models;

public partial class CityTable
{
    public long Id { get; set; }

    public string City { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
