using System;
using System.Collections.Generic;

namespace SimulasiAkhir.Models;

public partial class Loyalty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal RequiredPoint { get; set; }

    public int Multiplier { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
