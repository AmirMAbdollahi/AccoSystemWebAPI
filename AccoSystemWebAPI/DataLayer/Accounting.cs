using System;
using System.Collections.Generic;

namespace AccoSystemWebAPI.DataLayer;

public partial class Accounting
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public byte TransactionType { get; set; }

    public int Amount { get; set; }

    public string? Description { get; set; }

    public DateTime DateTime { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
