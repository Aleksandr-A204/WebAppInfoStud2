using System;
using System.Collections.Generic;

namespace WebAppInfoStud2.Models;

public partial class Address
{
    public long Id { get; set; }

    public long? CityId { get; set; }

    public long? PostindexId { get; set; }

    public long? StreetId { get; set; }

    public virtual CityTable? City { get; set; }

    public virtual PostindexTable? Postindex { get; set; }

    public virtual StreetTable? Street { get; set; }
}
