using System;
using System.Collections.Generic;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2;

public partial class Address
{
    public long Id { get; set; }

    public long CityId { get; set; }

    public long PostindexId { get; set; }

    public long StreetId { get; set; }

    public virtual CityTable City { get; set; } = null!;

    public virtual PostindexTable Postindex { get; set; } = null!;

    public virtual StreetTable Street { get; set; } = null!;
}
